using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerAudioController playerAudioController;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPoint;
	[SerializeField] private Vector2 groundCheckSize = new Vector2(0.45f, 0.03f);
    [SerializeField] private ParticleSystem dustTrail;
    [SerializeField] private ParticleSystem fallOnImpactEffect;
    [SerializeField] private ParticleSystem deathEffect;
    private ParticleSystem.EmissionModule dustEmission;

    private GameManager gameManager;

    //private bool canJump;
    private bool onGround;
    private bool wasOnGround;
    private bool facingRight;
    public bool canDoubleJump;
    private bool isJumping;
    public bool deathSoundIsPlaying = false;

    private float moveInput;
    private float coyoteTimeCounter;
    //private float lastJumpTime;

    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    //public float rememberLastJumpTime = 0.2f;
    public float coyoteTime = 0.15f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        dustEmission = dustTrail.emission;
        //GetComponent<Collider2D>();
        //Kittipat Sangka 633040479-0
    }

    private void Update() 
    {
        coyoteTimeCounter -= Time.deltaTime;
        //lastJumpTime -= Time.deltaTime;
        if(moveInput < 0 && !facingRight)
            {
                Turn();
            }
            if(moveInput > 0 && facingRight)
            {
                Turn();
            }
        PlayDustEffect();
        PlayFallOnImpactEffect();
    }

    private void FixedUpdate() 
    {
        CheckGround();
        Move();
        //Debug.Log(onGround);
        //Debug.Log(moveInput);
    }

    private IEnumerator SetZeroCoyoteTimeCounter()
    {
        yield return new WaitForSeconds(0.1f);
        coyoteTimeCounter = 0f;
    }

    private void PlayDustEffect()
    {
        if (moveInput != 0 && onGround)
            {
                dustEmission.rateOverTime = 35f;
            }
        else
            {
                dustEmission.rateOverTime = 0f;
            }
    }

    private void PlayFallOnImpactEffect()
    {
        if (!wasOnGround && onGround)
            {
                fallOnImpactEffect.gameObject.SetActive(true);
                fallOnImpactEffect.Stop();
                fallOnImpactEffect.transform.position = dustTrail.transform.position;
                fallOnImpactEffect.Play();
            }
        wasOnGround = onGround;
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && coyoteTimeCounter > 0)
        {
            StartCoroutine(SetZeroCoyoteTimeCounter());
            //lastJumpTime = rememberLastJumpTime; time for when press the button before land on the ground.
            //lastJumpTime = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse); 
            playerAudioController.PlayJumpSound();
        } 
        if (value.isPressed && canDoubleJump && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            canDoubleJump = false;
            playerAudioController.PlayJumpSound();
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
            isJumping = false;
            coyoteTimeCounter = coyoteTime;
            anim.SetBool("isGrounded", true);
        }
        else
        {
            isJumping = true;
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

    private void OnQuit(InputValue value)
    {
        if (value.isPressed)
        {
            if (gameManager == null)
            {
                gameManager = FindObjectOfType<GameManager>();
            }
            gameManager.playerHealth = 3;
            gameManager.EscapeToMainMenu();
        }
    }

    private IEnumerator FinishDeathSoundBeforeDeath()
    {
        deathSoundIsPlaying = true;
        playerAudioController.PlayDeathSound();
        yield return new WaitForSeconds(0.1f);
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        gameManager.playerHealth -= 1;
        deathSoundIsPlaying = false;
        yield return new WaitForSeconds(0.7f);
        gameManager.ProcessPlayerDeath();
    }

    public void TakeDamage()
    {
        deathEffect.gameObject.SetActive(true);
        deathEffect.Stop();
        deathEffect.transform.position = dustTrail.transform.position;
        deathEffect.Play();
        StartCoroutine(FinishDeathSoundBeforeDeath());
    }
}

