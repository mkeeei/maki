using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject director;
    public GameObject donutStyleObject;
    public AudioClip Ahiru;
    public AudioClip Donut;
    public AudioClip Cute; // 追加：Cute 音声
    public AudioClip PowerUp; // PowerUp音
    bool isPoweringUp = false; // パワーアップ中フラグ
    int duckHitCount = 0;  // 追加：アヒル衝突回数
    bool isRotating = false; // 回転中かどうか
    bool isCutePlaying = false; // Cute再生中かどうか

    AudioSource aud;
    Rigidbody rb;
    bool canJump = false;
    public float jumpPower = 1000f;

    public object Rigidbody { get; private set; }

    void Start()
    {
        Application.targetFrameRate = 60;
        this.director = GameObject.Find("GameDirector");
        this.aud = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

    }

    void OnTriggerEnter(Collider other)
    {
        // Cute再生中は、DuckやDonutの音をスキップ
        if (isPoweringUp || isCutePlaying)
        {
            Debug.Log("Cuteが再生中なので他の音はスキップします");
            return;
        }

        if (other.gameObject.CompareTag("ItemDonut"))
        {
            aud.PlayOneShot(Donut);
        }

        if (other.gameObject.CompareTag("ItemDuck"))
        {
            aud.PlayOneShot(Ahiru);
            canJump = true;

            duckHitCount++;

            if (duckHitCount == 5 && !isRotating)
            {
                StartCoroutine(RotateCute());
            }
            else if (duckHitCount == 10 && !isPoweringUp)
            {
                StartCoroutine(PlayPowerUp());
            }
        }
    }
    IEnumerator RotateCute()
    {
        isRotating = true;
        isCutePlaying = true;

        aud.clip = Cute;
        aud.Play();

        float duration = 1.2f;
        float elapsed = 0f;
        float totalRotation = 360f; // 1回転
        float rotationAmount = 0f;

        Vector3 originalPos = transform.position;
        Vector3 originalEuler = transform.eulerAngles; // 回転角度を保存

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            float deltaRotation = (totalRotation / duration) * Time.deltaTime;
            transform.Rotate(0f, deltaRotation, 0f);
            rotationAmount += deltaRotation;

            float jump = Mathf.Sin(t * Mathf.PI) * 0.4f;
            transform.position = originalPos + new Vector3(0, jump, 0);

            float scale = 1 + Mathf.Sin(t * Mathf.PI) * 0.4f;
            transform.localScale = new Vector3(scale, scale, scale);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 回転角度を正しくリセット（誤差吸収）
        transform.eulerAngles = originalEuler + new Vector3(0, totalRotation, 0);
        transform.position = originalPos;
        transform.localScale = Vector3.one;

        yield return new WaitForSeconds(Cute.length - duration);

        isRotating = false;
        isCutePlaying = false;
    }
    IEnumerator PlayPowerUp()
    {
        isPoweringUp = true;

        // PowerUp音再生
        aud.clip = PowerUp;
        aud.Play();

        float effectTime = 1.5f;
        float elapsed = 0f;

        Vector3 originalScale = transform.localScale;

        while (elapsed < effectTime)
        {
            // 体がバウンドする感じ
            float scaleFactor = 1 + Mathf.Sin(elapsed * 10f) * 0.2f; // ぷるぷる
            transform.localScale = originalScale * scaleFactor;

            // 色をキラキラにしたい場合（オプション）
            // GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.yellow, Mathf.Sin(elapsed * 20f));

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;

        Debug.Log("パワーアップ完了！");

        isPoweringUp = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);
                transform.position = new Vector3(x, 1, z);
            }
        }

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            Debug.Log("ジャンプした！");
        }
    }

    public void ChangeViewStyle(bool isDonutStyleD)
    {
        donutStyleObject.SetActive(isDonutStyleD);
    }
}

