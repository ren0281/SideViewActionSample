using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameClearText;

    //ゲームオブジェクトを取得して表示
    public void GameOver()
    {
        gameOverText.SetActive(true);

        //1.5秒後にリスタートする
        Invoke("RestartScene", 1.5f);
    }
    //上と同じ要領でクリア表示する
    public void GameClear()
    {
        gameClearText.SetActive(true);
        RestartScene();
    }

    //GameOver時にリスタートする
    void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

}
