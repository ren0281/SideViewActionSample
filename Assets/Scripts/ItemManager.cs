using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //アイテムは取得したら破壊する
    public void GetItem()
    {
        Destroy(this.gameObject);
    }
}
