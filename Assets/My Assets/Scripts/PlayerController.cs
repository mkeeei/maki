using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject director;
    private Rigidbody rb;
    private bool isCaught = false;

    void Start()
    {
        Application.targetFrameRate = 60;
        this.director = GameObject.Find("GameDirector");

        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    void OnTriggerEnter(Collider other)
    {

        // 衝突したオブジェクトのタグを確認
        if (other.CompareTag("UkiwaPrefab"))
        {
            // ドーナツに衝突した場合、スコアを加算し、UkiwaPrefabを削除
            this.director.GetComponent<GameDirector>().GetDonut();

            // アヒルに衝突した場合、スコアを加算し、UkiwaPrefabを削除
            this.director.GetComponent<GameDirector>().GetDuck();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            // アイテムを地面に残す処理
            transform.SetParent(other.transform);
            GetComponent<Rigidbody2D>().isKinematic = true;
        }

        if (other.CompareTag("UkiwaPrefab") && !isCaught)
        {
            isCaught = true;
            // アイテムを消す処理
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            // アイテムを地面に残す処理
            rb.isKinematic = true;
            // 必要に応じて、アイテムを地面に固定する処理を追加
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //animator.SetTrigger("RaiseHands");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);
                transform.position = new Vector3(x, 1, z);
            }
        }

    }
}
