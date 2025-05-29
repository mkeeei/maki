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

        // �Փ˂����I�u�W�F�N�g�̃^�O���m�F
        if (other.CompareTag("UkiwaPrefab"))
        {
            // �h�[�i�c�ɏՓ˂����ꍇ�A�X�R�A�����Z���AUkiwaPrefab���폜
            this.director.GetComponent<GameDirector>().GetDonut();

            // �A�q���ɏՓ˂����ꍇ�A�X�R�A�����Z���AUkiwaPrefab���폜
            this.director.GetComponent<GameDirector>().GetDuck();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            // �A�C�e����n�ʂɎc������
            transform.SetParent(other.transform);
            GetComponent<Rigidbody2D>().isKinematic = true;
        }

        if (other.CompareTag("UkiwaPrefab") && !isCaught)
        {
            isCaught = true;
            // �A�C�e������������
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            // �A�C�e����n�ʂɎc������
            rb.isKinematic = true;
            // �K�v�ɉ����āA�A�C�e����n�ʂɌŒ肷�鏈����ǉ�
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
