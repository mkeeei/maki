using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UkiwaController : MonoBehaviour

{
    GameObject player;
    void Start()
    {
        this.player = GameObject.Find("Ukiwa");
    }

    void Update()
    {
        // �t���[�����ɓ����ŗ���������
        transform.Translate(0, -0.1f, 0);

        // ��ʊO�ɏo����I�u�W�F�N�g��j������
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        // �����蔻��
        Vector3 p1 = transform.position;             // ��̒��S���W
        Vector3 p2 = this.player.transform.position; // �L�̒��S���W
        Vector3 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.5f;
        float r2 = 1.0f;

        if (d < r1 + r2)
        {
            // �ēX�N���v�g�Ƀv���C��ƏՓ˂������Ƃ�`����B
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp();

            // �Փ˂����ꍇ�́A�L������
            Destroy(gameObject);
            // Destroy(player);
        }
    }
}

