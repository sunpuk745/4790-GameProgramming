using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private float moveInput;
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public SpriteRenderer playerSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>();
        //Kittipat Sangka 633040479-0
    }

    private void Update() 
    {
        transform.position += new Vector3(moveInput, 0, 0) * Time.deltaTime * moveSpeed;
    }

    public void ChangeRed()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        playerSprite.color = Color.red;
    }

    public void ChangeGreen()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        playerSprite.color = Color.green;
    }

    public void ChangeBlue()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        playerSprite.color = Color.blue;
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
    }
}

