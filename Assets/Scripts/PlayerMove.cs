using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float RunSpeed = 2;

    public float JumpSpeed = 3;

    public float DoubleJumpSpeed = 2.5f;

    private bool canDobleJump;

    Rigidbody2D rb2D;

    public bool BetterJump = false;

    public float FallMultiplier = 0.5f;

    public float LowJumpMultiplier = 1f;

    public SpriteRenderer spriteRenderer;

    public Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey("space"))

        {
            if (CheckGround.isGrounded)
            {
                canDobleJump = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, JumpSpeed);
            }
            else
            {
                if (Input.GetKeyDown("space"))
                {
                    if (canDobleJump)
                    {
                        animator.SetBool("DoubleJump", true);
                        rb2D.velocity = new Vector2(rb2D.velocity.x, DoubleJumpSpeed);
                        canDobleJump=false;
                    }
                }
            }
        }



        if (!CheckGround.isGrounded)
        {
            animator.SetBool("Run", false);

            if (rb2D.velocity.y > 0)
            {
                animator.SetBool("Jump", true);
                animator.SetBool("Falling", false);
            }
            else if (rb2D.velocity.y < 0)
            {
                animator.SetBool("Jump", false);
                animator.SetBool("Falling", true);
            }
        }
        else
        {
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Falling", false);
        }
    }

    void FixedUpdate()
    {

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(RunSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
        }

        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.velocity = new Vector2(-RunSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Run", false);
        }

        if (BetterJump)
        {
            if (rb2D.velocity.y < 0)
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier) * Time.deltaTime;
            }

            if (rb2D.velocity.y > 0 && !Input.GetKey("space"))
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier) * Time.deltaTime;
            }
        }

    }

}
