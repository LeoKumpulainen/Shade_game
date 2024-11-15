using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    private float Move;


    [Header("Jumping")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float jump;
    [SerializeField] private int jumpPower;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float fallMultiplier;
    private bool isGrounded;
    private bool isJumping;
    private float jumpCounter;


    [Header("Dashing")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float startDashTime;
    [SerializeField]private float dashTime;
    private int directions;

    

    [Header("Wall sliding and Jumping")]
    [SerializeField] Transform wallCheck;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float wallSlidingSpeed;
    bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(25f, 9f);

    Vector2 vecGravity;

    private Rigidbody2D rb;

    void Start()
    {
        vecGravity = new Vector2 (0, -Physics2D.gravity.y);

        rb = GetComponent<Rigidbody2D>();

        dashTime = startDashTime;
    }

    void Update()
    {
        Dash();
        flip();
        Jumping();
        WallSlide();
        WallJump();


        if (!isWallSliding)
        {
            flip();
        }

        Move = Input.GetAxis("Horizontal");

    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(Move * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

        }
        Invoke(nameof(StopWallJumping), wallJumpingDuration);   
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void WallSlide()
    {
        if (isWalled() && !IsGrounded() && Move != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void Jumping()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow) && !IsGrounded() == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            isJumping = true;
            jumpCounter = 0;
        }

        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpM = jumpMultiplier * (1 - t); 
            }

            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }
    }

    private void Dash()
    {
        if (directions == 0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(Move < 0)
                {
                    directions = 1;
                }
                else if (Move > 0)
                {
                    directions = 2;
                }
            }
        }
        else
        {
            if(dashTime <= 0)
            {
                directions = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if(directions == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                }
                else if(directions == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
        }
    }


    void flip()
    {
        if (Move < -0.01f) transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        if (Move > 0.01f) transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

}