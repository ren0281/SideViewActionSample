using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameObject deathEffect;
    public enum DIRECTION_TIPE //動きの方向性を列挙型で宣言
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TIPE direction = DIRECTION_TIPE.STOP;　//通常時　＝　停止すると設定

    Rigidbody2D rigidbody2D;　//rigidbodyを定義？？
    float speed;　//早さを取得

    private void Start()　//rigidbodyを取得
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        //右へ
        direction = DIRECTION_TIPE.RIGHT;
    }

    private void Update()
    {
        if ( !IsGround())
        {
            //方向を変える
            ChangeDirectiom();
        }

    }

    private void FixedUpdate()
    {
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

    //エネミーの下に地面があるか判定する
    bool IsGround()
    {
        Vector3 startVec = transform.position + transform.right * 0.5f * transform.localScale.x;
        Vector3 endVec = startVec - transform.up * 0.5f;
        Debug.DrawLine(startVec, endVec);

        return Physics2D.Linecast(startVec, endVec, blockLayer);
    }

    //方向反転
    void ChangeDirectiom()
    {
        if(direction == DIRECTION_TIPE.RIGHT)
        {
            direction = DIRECTION_TIPE.LEFT;
        }
        else if (direction == DIRECTION_TIPE.LEFT)
        {
            direction = DIRECTION_TIPE.RIGHT;
        }
    }

    public void DestroyEnemy()
    {
        Instantiate(deathEffect, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}