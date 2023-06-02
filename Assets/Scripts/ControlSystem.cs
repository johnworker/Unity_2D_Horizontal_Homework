using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    [Header("���ʳt��"), Range(1, 10)]
    public float moveSpeed = 4.5f;
    [Header("���a����")]
    public Rigidbody2D rigPlayer;
    [Header("�ʵe���")]
    public Animator ani;
    [Header("�]�B�Ѽ�")]
    public string parRun = "���ʶ}��";
    [Header("���D")]
    public float jumpForce = 4000f;

    [Header("���D�Ѽ�")]
    public string parJumping = "���D�}��";

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
        // GetAxisRaw ������o-1 , 0 , 1 ��3�ӼƦr
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
            // �мW�j���D����
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
