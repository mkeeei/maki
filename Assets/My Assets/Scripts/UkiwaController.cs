using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UkiwaController : MonoBehaviour
{
    GameObject UkiwaPrefabs;
    public float dropSpeed = 100000f; // �������x


    void Update()
    {
        // �����ŗ���������
        transform.Translate(0, this.dropSpeed, 0);

        // ��ʊO�ɏo����I�u�W�F�N�g��j������
        if (transform.position.y < -1.0f)
        {
            Destroy(gameObject);
        }
    }


}
