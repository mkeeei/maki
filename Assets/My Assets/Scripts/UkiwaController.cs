using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UkiwaController : MonoBehaviour
{
    GameObject UkiwaPrefabs;
    public float dropSpeed = 100000f; // 落下速度


    void Update()
    {
        // 等速で落下させる
        transform.Translate(0, this.dropSpeed, 0);

        // 画面外に出たらオブジェクトを破棄する
        if (transform.position.y < -1.0f)
        {
            Destroy(gameObject);
        }
    }


}
