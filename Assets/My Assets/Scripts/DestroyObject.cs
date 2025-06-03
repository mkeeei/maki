using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    GameDirector _gameDirector;

    public void Init(GameDirector gameDirector)
    {
        _gameDirector = gameDirector;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("ItemDonut"))
        {

            // ドーナツに衝突した場合スコアを加算
            _gameDirector.GetDonut();
            DeleteItem(other.gameObject);
        }

        if (other.gameObject.CompareTag("ItemDuck"))
        {

            // アヒルに衝突した場合スコアを加算
            GetComponent<ParticleSystem>().Play();
            _gameDirector.GetDuck();
            DeleteItem(other.gameObject);
        }
    }

    /// <summary>
    /// アイテムを消す処理
    /// </summary>
    /// <param name="item">落下してきたアイテム</param>
    void DeleteItem(GameObject item)
    {
        // アイテムを消す処理
        Destroy(item);

    }

}