using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPoint;
	[SerializeField] private Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);

    private bool canJump;
    private bool onGround;
    private bool facingRight;

    private float moveInput;

    public int playerHealth = 1;

    public float moveSpeed = 10f;
    public float jumpForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //GetComponent<Collider2D>();
        //Kittipat Sangka 633040479-0
    }

    private void Update() 
    {
        if(moveInput < 0 && !facingRight)
            {
                Turn();
            }
            if(moveInput > 0 && facingRight)
            {
                Turn();
            }
    }

    private void FixedUpdate() 
    {
        CheckGround();
        Move();
        //Debug.Log(onGround);
        //Debug.Log(moveInput);
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && canJump)
        {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset the y-force to prevent player stacking up jump momentum.
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    private void CheckGround()
    {
        onGround = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        if (onGround)
        {
            canJump = true;
            anim.SetBool("isGrounded", true);
        }
        else
        {
            canJump = false;
            anim.SetBool("isGrounded", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
    }

    private void Turn()
	{
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;
		facingRight = !facingRight;
	}

    public void TakeDamage()
    {
        playerHealth -= 1;
        //Debug.Log(playerHealth);
    }
}

