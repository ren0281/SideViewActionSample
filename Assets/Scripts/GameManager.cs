using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameClearText;
    [SerializeField] Text ScoreText;

    //アイテム取得時のスコア加算式

    const int MAX_SCORE = 9999;
    int score = 0;

    //開始時のスコアを0で固定
    private void Start()
    {
        ScoreText.text = score.ToString();
    }

    public void AddScore(int val)
    {
        score += val;
        if ( score > MAX_SCORE )
        {
            score = MAX_SCORE;
        }

        //ScoreTextに反映するがint型になっているのでString型にする必要がある
        ScoreText.text = score.ToString();
    }

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
