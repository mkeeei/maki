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
    [SerializeField] TextMeshProUGUI countdownText; // �J�E���g�_�E���p��TextMeshProUGUI
    [SerializeField] TextMeshProUGUI timeUpText; // Time UP�p��TextMeshProUGUI
    [SerializeField] UkiwaGenerator generator;
    [SerializeField] DestroyObject _destroyController;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private float moveDistance = 100f;
    [SerializeField] GameObject player; // �v���C���[��GameObject�iPlayerController�����Ă�I�u�W�F�N�g�j
    [SerializeField] float playerMoveDuration = 3f; // ������蓮������
    [SerializeField] float zoomAmount = 30f; // FOV�Y�[����
    [SerializeField] float zoomDuration = 1.5f;
    public AudioClip[] timeUpClips;  // �Đ��������������C���X�y�N�^�[�ŃZ�b�g
    AudioSource timeUpAudioSource;  // ��p��AudioSource
    bool timeUpAudioPlayed = false; // ��x�����Đ����邽�߂̃t���O

    void Awake()
    {
        _destroyController.Init(this);
        countdownText.gameObject.SetActive(false); // ������Ԃł͔�\��
        timeUpText.gameObject.SetActive(false); // ������Ԃł͔�\��
        timeUpAudioSource = gameObject.AddComponent<AudioSource>(); // �� �ǉ�

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

        // �c��5�b����J�E���g�_�E����\��
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
                StartCoroutine(PlayTimeUpAudioAndMovePlayerAfterDelay(3f)); // �� �����ɕύX
                timeUpAudioPlayed = true;
            }
        }
    }

    IEnumerator PlayTimeUpAudioAndMovePlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // ����Audio��~
        foreach (AudioSource a in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            if (a != timeUpAudioSource) a.Stop();
        }

        // �����_�������Đ�
        if (timeUpClips.Length > 0)
        {
            int index = Random.Range(0, timeUpClips.Length);
            timeUpAudioSource.PlayOneShot(timeUpClips[index]);
        }
        if (player != null)
        {
            Camera cam = Camera.main;
            // nearClipPlane �������߂Â��Č��؂�h�~�i�K�v�ɉ����Ē����j
            cam.nearClipPlane = 0.1f;

            // �Y�[���������FOV���l�����A�����𒲐�
            float targetDistance = 2.5f;

            // �v���C���[���J�����̐��ʂɈړ�
            Vector3 targetPos = GetPlayerViewPosition(cam, player.transform, targetDistance);

            // �v���C���[�ړ�
            player.transform.DOMove(targetPos, playerMoveDuration).SetEase(Ease.InOutSine);

            // �v���C���[�𒍎�����J������]
            StartCoroutine(LookAtPlayerCoroutine(player.transform, cam.transform, zoomDuration));

            // �J�����Y�[��
            StartCoroutine(ZoomCamera(cam, zoomAmount, zoomDuration));
        }
    }

    // player���m���ɉf��ʒu���v�Z
    Vector3 GetPlayerViewPosition(Camera cam, Transform player, float distanceFromCamera)
    {
        Vector3 basePos = cam.transform.position + cam.transform.forward * distanceFromCamera;

        // �v���C���[�̍������l���i�o�E���f�B���O�{�b�N�X�̒����j
        Renderer rend = player.GetComponent<Renderer>();
        float playerHeight = (rend != null) ? rend.bounds.size.y : 2f; // �ڈ�2m

        Vector3 playerCenterOffset = new Vector3(0, playerHeight / 2f, 0);

        return new Vector3(basePos.x, player.position.y + playerCenterOffset.y, basePos.z);
    }
    // �J������player�����ɂ�������]������R���[�`��
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

