using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //ゲームマネージャーを取得
    [SerializeField] GameManager gameManager;

    //ブロックレイヤーを取得
    [SerializeField] LayerMask blockLayer;
    public enum DIRECTION_TIPE //動きの方向性を列挙型で宣言
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TIPE direction = DIRECTION_TIPE.STOP;　//通常時　＝　停止すると設定

    Rigidbody2D rigidbody2D;　//rigidbodyを定義？？
    float speed;　//早さを取得

    Animator animator;

    float jumpPower = 400; //Jumpする際の加える力を定義

    bool isDead = false;

    private void Start()　//rigidbody・animatorを取得
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");　//水平方向の移動量を入力(方向キー)
        animator.SetFloat("speed", Mathf.Abs(x));

        if (x == 0)
        {
            if(isDead)
            {
                return;
            }

            //止まっている
            direction = DIRECTION_TIPE.STOP;
        }
        else if(x > 0)
        {
            //右へ
            direction = DIRECTION_TIPE.RIGHT;
        }
        else if(x < 0)
        {
            //左へ
            direction = DIRECTION_TIPE.LEFT;
        }
        //spaceが押されたらジャンプ
        if (IsGround())
        {
            if (Input.GetKeyDown("space"))
            {
                Jump();
            }
            else
            //空中にいる
            {
                animator.SetBool("isJumping", false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }


        switch (direction)　//入力量によって宣言したDIRECTIONを切り替える
        {
            case DIRECTION_TIPE.STOP:　//入力が無ければ停止
                speed = 0;
                break;

            case DIRECTION_TIPE.RIGHT:　//右へ入力があれば右を向いて右へ移動
                speed = 3;
                transform.localScale = new Vector3(1, 1, 1);
                break;

            case DIRECTION_TIPE.LEFT:　//左へ入力があれば左を向いて左へ移動
                speed = -3;
                transform.localScale = new Vector3(-1, 1, 1);
                break;

        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    void Jump()
    {
        //上に力を加える
        rigidbody2D.AddForce(Vector2.up * jumpPower);
        animator.SetBool("isJumping", true);
        //スペースキーによる入力回数を調べるために記載
        Debug.Log("Jump");
    }
     bool IsGround() //地面に触れているか判定する
    {
        //　始点と終点を作成（ベクトル的に）
        Vector3 leftStartpoint = transform.position - Vector3.right * 0.2f;
        Vector3 rightStartpoint = transform.position + Vector3.right * 0.2f;
        Vector3 endPoint = transform.position - Vector3.up * 0.1f;

        //ベクトルを可視化するために記載
        Debug.DrawLine(leftStartpoint, endPoint);
        Debug.DrawLine(rightStartpoint, endPoint);

        return Physics2D.Linecast(leftStartpoint, endPoint, blockLayer)
            || Physics2D.Linecast(rightStartpoint, endPoint, blockLayer);

    }

    //Unity既存のコードを利用してゲームオーバーの判定を設定
    //↓は2Dのものが当たったときに自動で実行される関数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
        {
            return;
        }

        if (collision.gameObject.tag == "Trap")
        {
            Debug.Log("Game Over");
            PlayerDeath();
        }

        //ゲームクリアも上記と同じように実装する
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Game Clear!!");
            gameManager.GameClear();
        }

        //アイテム取得
        if (collision.gameObject.tag == "Item")
        {
            //条件式が成り立った場合、ItemManager内のGetItemというメソッドを実行する
            collision.gameObject.GetComponent<ItemManager>().GetItem();
        }

        //エネミーとの当たり判定
        if(collision.gameObject.tag == "Enemy")
        {
            EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();

            if(this.transform.position.y + 0.2f > enemy.transform.position.y)
            {
                //上から踏んだ場合
                //縦方向の速度を0にする
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
                //ジャンプする
                Jump();
                //敵を削除
                enemy.DestroyEnemy();

            }
            else
            {
                //横でぶつかったらゲームオーバー
                Destroy(this.gameObject);
                PlayerDeath();
            }
        }

    }

    void PlayerDeath()
    {
        isDead = true;
        rigidbody2D.velocity = new Vector2(0, 0);
        rigidbody2D.AddForce(Vector2.up * jumpPower);
        animator.Play("PlayerDeathAnimation");
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        Destroy(boxCollider2D);

        gameManager.GameOver();
    }
}
