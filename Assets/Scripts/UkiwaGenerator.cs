using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class UkiwaGenerator : MonoBehaviour
{
    //public GameObject DonutPrefab;
    //float span = 1.0f;
    //float delta = 0;
    //int ratio = 2;
    //float speed = -0.03f;

    public GameObject[] UkiwaPrefabs = new GameObject[5]; // Ukiwa�̃v���n�u���i�[����z��
    public float spawnHeight = 50f; // �������鍂��
    public Vector2 spawnRangeX = new Vector2(0,50f); // ��������X���W�͈̔�
    public Vector2 spawnRangeZ = new Vector2(0,50f); // ��������Z���W�͈̔�

    float span = 2.0f;
    float delta = 0;

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;

            // Ukiwa�̃v���n�u�������_���ɑI��
            int index = Random.Range(0, UkiwaPrefabs.Length);
            Debug.Log(index);
            GameObject selectedPrefab = UkiwaPrefabs[index];

            // �����_����X���W�𐶐�
            float x = Random.Range(spawnRangeX.x, spawnRangeX.y);

            // �����_����Z���W�𐶐�
            float z = Random.Range(spawnRangeZ.x, spawnRangeZ.y);

            // Ukiwa�𐶐�
            Instantiate(selectedPrefab, new Vector3(x, spawnHeight, z), Quaternion.identity);
        }
    }
}

//    public void SetParameter(float span, float speed, int ratio)
//    {
//        this.span = span;
//        this.speed = speed;
//        this.ratio = ratio;
//    }
//    public GameObject[] Ukiwa;
//    public int number;

//    void Start()
//    {
//        number = Random.Range(0, Ukiwa.Length);
//        //Instantiate(Ukiwa[number], transform.position, transform.rotation);
//    }

//void Update()
//    {
//        this.delta += Time.deltaTime;
//        if (this.delta > this.span)
//        {
//            this.delta = 0;
//            GameObject item;
            
//            item = Instantiate(DonutPrefab, new Vector3(0, 50, 0), DonutPrefab.transform.rotation);
//            // �o�ߎ��ԃ��Z�b�g
//            span = 0f;
//            float x = Random.Range(-1, 2);
//            float z = Random.Range(-1, 2);
//            //item.GetComponent<UkiwaController>().dropSpeed = speed;
//        }
//    }

