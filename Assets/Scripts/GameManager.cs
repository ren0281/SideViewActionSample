using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameClearText;

    //ゲームオブジェクトを取得して表示
    public void GameOver()
    {
        gameOverText.SetActive(true);
    }
    //上と同じ要領でクリア表示する
    public void GameClear()
    {
        gameClearText.SetActive(true);
    }

}
