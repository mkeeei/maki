using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject director;
    public AudioClip Ahiru;
    public AudioClip Donut;
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
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("ItemDonut"))
        {

            // ドーナツに衝突した場合→音なる
            this.aud.PlayOneShot(this.Donut);
        }

        if (other.gameObject.CompareTag("ItemDuck"))
        {

            // アヒルに衝突した場合→音なる
            this.aud.PlayOneShot(this.Ahiru);
            canJump = true; // ジャンプ可能にする
        }
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
}

