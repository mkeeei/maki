using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UkiwaGeneator : MonoBehaviour
{
    public GameObject DonutPrefab;
    float span = 1.0f;
    float delta = 0;
    int ratio = 2;
    float speed = -0.03f;

    public void SetParameter(float span, float speed, int ratio)
    {
        this.span = span;
        this.speed = speed;
        this.ratio = ratio;
    }
    public GameObject[] Ukiwa;
    public int number;

    void Start()
    {
        number = Random.Range(0, Ukiwa.Length);
        Instantiate(Ukiwa[number], transform.position, transform.rotation);
    }

void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject item;
            
            item = Instantiate(DonutPrefab, new Vector3(60, 35, 550), DonutPrefab.transform.rotation);
            // 経過時間リセット
            span = 0f;
            float x = Random.Range(-1, 2);
            float z = Random.Range(-1, 2);
            item.GetComponent<UkiwaController>().dropSpeed = speed;
        }
    }
}
