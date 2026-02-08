using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float jumpForce;
    public bool isGrounded;
    public bool isDown;

    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isGround", true);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        anim.SetBool("isGround", isGrounded);
    }
}
