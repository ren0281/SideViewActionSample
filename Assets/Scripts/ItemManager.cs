using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //GameManagerを呼ぶ
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //アイテムは取得したら破壊する
    public void GetItem()
    {
        gameManager.AddScore(100);
        Destroy(this.gameObject);
    }
}
