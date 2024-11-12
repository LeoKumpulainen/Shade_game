using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    private float Move;
    public float jump;
    public bool isJumping;

    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashlength;
    public float dashCooldown;

    private float dashCounter;
    private float dashCoolCounter;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        activeMoveSpeed = speed;
    }

    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(activeMoveSpeed * Move, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashlength;
            }
        }

        if (dashCounter >= 0)
        { 
            dashCounter -= Time.deltaTime; 
            
            if(dashCounter <= 0)
            {
                activeMoveSpeed = speed;
                dashCoolCounter = dashCooldown;
            }
        }
        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }
    //Jumping//
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
    
}
