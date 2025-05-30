using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    float time = 30.0f;
    int point = 0;
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] UkiwaGenerator generator;
    [SerializeField] DestroyObject _destroyController;

    void Awake()
    {
        _destroyController.Init(this);
    }

    public void GetDonut()
    {
        this.point += 100;
    }
    public void GetDuck()
    {
        this.point += 10000;
    }


    void Update()
    {
        this.time -= Time.deltaTime;

        if (this.time < 0)
        {
            this.time = 0;
            this.generator.SetParameter(10000.0f, 0, 0);
        }
        else if (0 <= this.time && this.time < 4)
        {
            this.generator.SetParameter(0.3f, -0.06f, 0);
        }
        else if (4 <= this.time && this.time < 12)
        {
            this.generator.SetParameter(0.5f, -0.05f, 6);
        }
        else if (8 <= this.time && this.time < 23)
        {
            this.generator.SetParameter(0.8f, -0.04f, 4);
        }
        else if (12 <= this.time && this.time < 60)
        {
            this.generator.SetParameter(1.0f, -0.03f, 2);
        }
        this.pointText.text = this.point.ToString() + " Point";
    }
}
