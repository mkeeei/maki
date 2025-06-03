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

            // �h�[�i�c�ɏՓ˂����ꍇ�X�R�A�����Z
            _gameDirector.GetDonut();
            DeleteItem(other.gameObject);
        }

        if (other.gameObject.CompareTag("ItemDuck"))
        {

            // �A�q���ɏՓ˂����ꍇ�X�R�A�����Z
            GetComponent<ParticleSystem>().Play();
            _gameDirector.GetDuck();
            DeleteItem(other.gameObject);
        }
    }

    /// <summary>
    /// �A�C�e������������
    /// </summary>
    /// <param name="item">�������Ă����A�C�e��</param>
    void DeleteItem(GameObject item)
    {
        // �A�C�e������������
        Destroy(item);

    }

}