using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Animator animator;
    //public AudioClip donutSE;
    //public AudioClip duckSE;
    //AudioSource aud;
    GameObject director;
    void Start()
    {
        Application.targetFrameRate = 60;
        this.director = GameObject.Find("GameDirector");

        // アニメーションでつけた手をあげる動作を実装
        //animator = GetComponent<Animator>();

        //this.aud = GetComponent<AudioSource>();
        //this.director = GameObject.Find("GameDirector");
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.activeSelf)
        //{
        //    //this.aud.PlayOneShot(this.donutSE);
        //    this.director.GetComponent<GameDirector>().GetDonut();
        //}
        //else
        //{
        //    //this.aud.PlayOneShot(this.duckSE);
        //    this.director.GetComponent<GameDirector>().GetDuck();
        //}
        //Destroy(other.gameObject);

        // 衝突したオブジェクトのタグを確認
        if (other.CompareTag("donutPrefab"))
        {
            // ドーナツに衝突した場合、スコアを加算し、UkiwaPrefabを削除
            this.director.GetComponent<GameDirector>().GetDonut();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("duckPrefab"))
        {
            // アヒルに衝突した場合、スコアを加算し、UkiwaPrefabを削除
            this.director.GetComponent<GameDirector>().GetDuck();
            Destroy(other.gameObject);
        }
        // それ以外のオブジェクトには何もしない
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
