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
    public float jumpForce = 4000f;

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

        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        ani.SetBool(parRun, h != 0);

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // 請增強跳躍高度
            rigPlayer.AddForce(new Vector2(0f, jumpForce));
            isJumping = true;
            ani.SetBool(parJumping, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            ani.SetBool(parJumping, false);
        }
    }

}
