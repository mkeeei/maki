using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class UkiwaGenerator : MonoBehaviour
{
    public GameObject[] UkiwaPrefabs = new GameObject[2]; // Ukiwaのプレハブを格納する配列
    public float spawnHeight = 9f; // 生成する高さ
    public Vector2 spawnRangeX = new Vector2(-6, 7f); // 生成するX座標の範囲
    public Vector2 spawnRangeZ = new Vector2(0, 5f); // 生成するZ座標の範囲

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

            //// Ukiwaのプレハブをランダムに選択
            int index = Random.Range(0, UkiwaPrefabs.Length);
            Debug.Log(index);
            GameObject selectedPrefab = UkiwaPrefabs[index];

            // ランダムなX座標を生成
            float x = Random.Range(spawnRangeX.x, spawnRangeX.y);

            // ランダムなZ座標を生成
            float z = Random.Range(spawnRangeZ.x, spawnRangeZ.y);

            // Ukiwaを生成
            Instantiate(selectedPrefab, new Vector3(x, spawnHeight, z), Quaternion.identity);


        }


    }
}


