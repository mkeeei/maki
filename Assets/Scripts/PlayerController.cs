using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public AudioClip donutSE;
    public AudioClip duckSE;
    AudioSource aud;
    GameObject director;
    void Start()
    {
        // アニメーションでつけた手をあげる動作を実装
        animator = GetComponent<Animator>();

        Application.targetFrameRate = 60;
        this.aud = GetComponent<AudioSource>();
        this.director = GameObject.Find("GameDirector");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Donut")
        {
            this.aud.PlayOneShot(this.donutSE);
            this.director.GetComponent<GameDirector>().GetDonut();
        }
        else if (other.gameObject.tag == "Duck")
        {
            this.aud.PlayOneShot(this.duckSE);
            this.director.GetComponent<GameDirector>().GetDuck();
        }
        Destroy(other.gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("RaiseHands");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);
                transform.position = new Vector3(x, 0, z);
            }
        }
    }
}
