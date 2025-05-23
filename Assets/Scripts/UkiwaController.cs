using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UkiwaController : MonoBehaviour

{
    GameObject player;
    public float dropSpeed = -0.03f;
    void Start()
    {
        this.player = GameObject.Find("Ukiwa");
    }
    void Update()
    {
        // 等速で落下させる
        transform.Translate(0, this.dropSpeed, 0);

        // 画面外に出たらオブジェクトを破棄する
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        // 当たり判定
        Vector3 p1 = transform.position;             // 中心座標
        Vector3 p2 = this.player.transform.position; // 中心座標
        Vector3 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.5f;
        float r2 = 1.0f;

        if (d < r1 + r2)
        {
            // 監督スクリプトにプレイやと衝突したことを伝える。
            GameObject director = GameObject.Find("GameDirector");

        }
    }
}

