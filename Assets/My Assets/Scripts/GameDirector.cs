using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameDirector : MonoBehaviour
{
    float time = 30.0f;
    int point = 0;
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] TextMeshProUGUI countdownText; // �J�E���g�_�E���p��TextMeshProUGUI
    [SerializeField] TextMeshProUGUI timeUpText; // Time UP�p��TextMeshProUGUI
    [SerializeField] UkiwaGenerator generator;
    [SerializeField] DestroyObject _destroyController;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private float moveDistance = 100f;


    void Awake()
    {
        _destroyController.Init(this);
        countdownText.gameObject.SetActive(false); // ������Ԃł͔�\��
        timeUpText.gameObject.SetActive(false); // ������Ԃł͔�\��
    }

    public void GetDonut()
    {
        this.point += 100;
    }
    public void GetDuck()
    {
        this.point += 1000;
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

        // �c��5�b����J�E���g�_�E����\��
        if (this.time <= 5 && this.time > 0)
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = Mathf.CeilToInt(this.time).ToString();
        }
        else
        {
            countdownText.gameObject.SetActive(false);
        }

        // �J�E���g�_�E���I����ɁuTime UP�v��\��
        if (this.time <= 0 && !timeUpText.gameObject.activeSelf)
        {
            timeUpText.gameObject.SetActive(true);
            timeUpText.text = "Time UP!";
            // �����ʒu��ݒ�
            Vector3 initialPosition = timeUpText.transform.position;
            timeUpText.transform.position = new Vector3(initialPosition.x, initialPosition.y - moveDistance, initialPosition.z);

            //// �J�����Ɍ������Ĉړ�
            timeUpText.transform.DOMove(initialPosition, moveDuration).SetEase(Ease.OutQuad);
        }
    }

}
