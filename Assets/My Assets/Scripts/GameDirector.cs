using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameDirector : MonoBehaviour
{
    float time = 30.0f;
    int point = 0;
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] TextMeshProUGUI countdownText; // カウントダウン用のTextMeshProUGUI
    [SerializeField] TextMeshProUGUI timeUpText; // Time UP用のTextMeshProUGUI
    [SerializeField] UkiwaGenerator generator;
    [SerializeField] DestroyObject _destroyController;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private float moveDistance = 100f;
    [SerializeField] GameObject player; // プレイヤーのGameObject（PlayerControllerがついてるオブジェクト）
    [SerializeField] float playerMoveDuration = 3f; // ゆっくり動く時間
    [SerializeField] float zoomAmount = 30f; // FOVズーム先
    [SerializeField] float zoomDuration = 1.5f;
    public AudioClip[] timeUpClips;  // 再生したい音声をインスペクターでセット
    AudioSource timeUpAudioSource;  // 専用のAudioSource
    bool timeUpAudioPlayed = false; // 一度だけ再生するためのフラグ

    void Awake()
    {
        _destroyController.Init(this);
        countdownText.gameObject.SetActive(false); // 初期状態では非表示
        timeUpText.gameObject.SetActive(false); // 初期状態では非表示
        timeUpAudioSource = gameObject.AddComponent<AudioSource>(); // ← 追加

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

    }
    public void GetDonut()
    {
        this.point += 100;
    }
    public void GetDuck()
    {
        this.point += 1000;
    }

    void Update()
    {
        this.time -= Time.deltaTime;

        if (this.time < 0)
        {
            this.time = 0;
            this.generator.SetParameter(10000.0f, 0, 0);
        }
        else if (0 <= this.time && this.time < 4)
        {
            this.generator.SetParameter(0.3f, -0.06f, 0);
        }
        else if (4 <= this.time && this.time < 12)
        {
            this.generator.SetParameter(0.5f, -0.05f, 6);
        }
        else if (8 <= this.time && this.time < 23)
        {
            this.generator.SetParameter(0.8f, -0.04f, 4);
        }
        else if (12 <= this.time && this.time < 60)
        {
            this.generator.SetParameter(1.0f, -0.03f, 2);
        }
        this.pointText.text = this.point.ToString() + " Point";

        // 残り5秒からカウントダウンを表示
        if (this.time <= 5 && this.time > 0)
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = Mathf.CeilToInt(this.time).ToString();
        }
        else
        {
            countdownText.gameObject.SetActive(false);
        }

        if (this.time <= 0 && !timeUpText.gameObject.activeSelf)
        {
            timeUpText.gameObject.SetActive(true);
            timeUpText.text = "Time UP!";

            Vector3 initialPosition = timeUpText.transform.position;
            timeUpText.transform.position = new Vector3(initialPosition.x, initialPosition.y - moveDistance, initialPosition.z);
            timeUpText.transform.DOMove(initialPosition, moveDuration).SetEase(Ease.OutQuad);

            if (!timeUpAudioPlayed)
            {
                StartCoroutine(PlayTimeUpAudioAndMovePlayerAfterDelay(3f)); // ← ここに変更
                timeUpAudioPlayed = true;
            }
        }
    }

    IEnumerator PlayTimeUpAudioAndMovePlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 他のAudio停止
        foreach (AudioSource a in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            if (a != timeUpAudioSource) a.Stop();
        }

        // ランダム音声再生
        if (timeUpClips.Length > 0)
        {
            int index = Random.Range(0, timeUpClips.Length);
            timeUpAudioSource.PlayOneShot(timeUpClips[index]);
        }
        if (player != null)
        {
            Camera cam = Camera.main;
            // nearClipPlane を少し近づけて見切れ防止（必要に応じて調整）
            cam.nearClipPlane = 0.1f;

            // ズーム完了後のFOVを考慮し、距離を調整
            float targetDistance = 2.5f;

            // プレイヤーをカメラの正面に移動
            Vector3 targetPos = GetPlayerViewPosition(cam, player.transform, targetDistance);

            // プレイヤー移動
            player.transform.DOMove(targetPos, playerMoveDuration).SetEase(Ease.InOutSine);

            // プレイヤーを注視するカメラ回転
            StartCoroutine(LookAtPlayerCoroutine(player.transform, cam.transform, zoomDuration));

            // カメラズーム
            StartCoroutine(ZoomCamera(cam, zoomAmount, zoomDuration));
        }
    }

    // playerが確実に映る位置を計算
    Vector3 GetPlayerViewPosition(Camera cam, Transform player, float distanceFromCamera)
    {
        Vector3 basePos = cam.transform.position + cam.transform.forward * distanceFromCamera;

        // プレイヤーの高さを考慮（バウンディングボックスの中央）
        Renderer rend = player.GetComponent<Renderer>();
        float playerHeight = (rend != null) ? rend.bounds.size.y : 2f; // 目安2m

        Vector3 playerCenterOffset = new Vector3(0, playerHeight / 2f, 0);

        return new Vector3(basePos.x, player.position.y + playerCenterOffset.y, basePos.z);
    }
    // カメラをplayer方向にゆっくり回転させるコルーチン
    IEnumerator LookAtPlayerCoroutine(Transform player, Transform cameraTransform, float duration)
    {
        Quaternion startRotation = cameraTransform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(player.position - cameraTransform.position);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            cameraTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraTransform.rotation = targetRotation;
    }
   
    IEnumerator ZoomCamera(Camera cam, float targetFOV, float zoomTime)
    {
        float startFOV = cam.fieldOfView;
        float elapsed = 0f;

        while (elapsed < zoomTime)
        {
            cam.fieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsed / zoomTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.fieldOfView = targetFOV;
    }
}

