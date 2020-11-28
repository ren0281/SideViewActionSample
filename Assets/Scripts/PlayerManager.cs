using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum DIRECTION_TIPE //動きの方向性を列挙型で宣言
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TIPE direction = DIRECTION_TIPE.STOP;　//通常時　＝　停止すると設定

    Rigidbody2D rigidbody2D;　//rigidbodyを定義？？
    float speed;　//早さを取得

    float jumpPower = 400; //Jumpする際の加える力を定義

    private void Start()　//rigidbodyを取得
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");　//水平方向の移動量を入力

        if (x == 0)
        {
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
        //spaceが押されたらJumpする
        if (Input.GetKeyDown("space"))
        {
            Jump();
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

    void Jump()
    {
        //上に力を加える
        rigidbody2D.AddForce(Vector2.up * jumpPower);
    }
}
