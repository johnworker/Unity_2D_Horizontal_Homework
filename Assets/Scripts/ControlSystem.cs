using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    [Header("移動速度"), Range(1, 10)]
    public float moveSpeed = 4.5f;
    [Header("玩家剛體")]
    public Rigidbody2D rigPlayer;
    [Header("動畫控制器")]
    public Animator ani;
    [Header("跑步參數")]
    public string parRun = "移動開關";
    [Header("跳躍")]
    public float jumpForce = 20f;

    public float maxJumpTime = 0.5f; // 最大跳躍時間
    private float jumpTime = 0.0f; // 跳躍已經持續的時間

    [Header("跳躍參數")]
    public string parJumping = "跳躍開關";

    private bool isJumping = false;

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // GetAxisRaw 直接獲得-1 , 0 , 1 的3個數字
        float facedirection = Input.GetAxisRaw("Horizontal");

        rigPlayer.velocity = new Vector2(h, 0) * moveSpeed;

        // 如果面向值 不等於 0
        if(facedirection != 0)
        {
            // 轉換.縮放 = 新 三維向量(面向值, 1, 1);
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        ani.SetBool(parRun, h != 0);

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rigPlayer.velocity = new Vector2(rigPlayer.velocity.x, jumpForce);
            isJumping = true;
            jumpTime = 0.0f;
            ani.SetBool(parJumping, true);
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTime < maxJumpTime)
            {
                rigPlayer.velocity = new Vector2(rigPlayer.velocity.x, jumpForce);
                jumpTime += Time.deltaTime;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            ani.SetBool(parJumping, false);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰撞函數.遊戲物件.比較標籤("地板")
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            ani.SetBool(parJumping, false);
        }
    }

}
