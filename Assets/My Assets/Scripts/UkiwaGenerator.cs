using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class UkiwaGenerator : MonoBehaviour
{
    public GameObject[] UkiwaPrefabs = new GameObject[2]; // Ukiwa�̃v���n�u���i�[����z��
    public float spawnHeight = 9f; // �������鍂��
    public Vector2 spawnRangeX = new Vector2(-6, 7f); // ��������X���W�͈̔�
    public Vector2 spawnRangeZ = new Vector2(0, 5f); // ��������Z���W�͈̔�

    public GameObject donutPrefab;
    public GameObject duckPrefab;
    float span = 2.0f;
    float delta = 0;
    float speed = -0.03f;
    int ratio = 2;

    public void SetParameter(float span, float speed, int ratio)
    {
        this.span = span;
        this.speed = speed;
        this.ratio = ratio;
    }
    
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;

            //// Ukiwa�̃v���n�u�������_���ɑI��
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


