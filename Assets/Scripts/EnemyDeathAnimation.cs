using UnityEngine;

public class EnemyDeathAnimation : MonoBehaviour
{
    public void OnCompleteAnimation()
    {
        Destroy(this.gameObject);
    }
}
