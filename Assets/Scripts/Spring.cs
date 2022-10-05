using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private Animator springAnim;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private SpringAudioController springAudioController;

    [SerializeField] private float springPower = 25;

    private void Start() 
    {
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        springAnim = gameObject.GetComponent<Animator>();
        springAudioController = gameObject.GetComponent<SpringAudioController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(PlayerTag))
        {
            springAudioController.PlaySprungSound();
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f); // Reset the y-force to prevent player stacking up jump momentum.
            playerRB.AddForce(springPower * Vector2.up, ForceMode2D.Impulse);
            springAnim.ResetTrigger("springTrigger");
            springAnim.SetTrigger("springTrigger");
        }
    }
}
