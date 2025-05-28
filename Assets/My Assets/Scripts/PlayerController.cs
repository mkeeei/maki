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

        // �A�j���[�V�����ł�����������铮�������
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

        // �Փ˂����I�u�W�F�N�g�̃^�O���m�F
        if (other.CompareTag("donutPrefab"))
        {
            // �h�[�i�c�ɏՓ˂����ꍇ�A�X�R�A�����Z���AUkiwaPrefab���폜
            this.director.GetComponent<GameDirector>().GetDonut();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("duckPrefab"))
        {
            // �A�q���ɏՓ˂����ꍇ�A�X�R�A�����Z���AUkiwaPrefab���폜
            this.director.GetComponent<GameDirector>().GetDuck();
            Destroy(other.gameObject);
        }
        // ����ȊO�̃I�u�W�F�N�g�ɂ͉������Ȃ�
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
