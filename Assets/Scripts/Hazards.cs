using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    private PlayerController player;
    private GameManager gameManager;

    private const string PlayerTag = "Player";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
        {
            //Debug.Log(player.playerHealth);
            gameManager.TakeDamage();
            gameManager.ProcessPlayerDeath();
        }
    }
}
