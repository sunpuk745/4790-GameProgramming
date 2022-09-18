using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    private GameManager gameManager;

    private const string PlayerTag = "Player";

    private void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag(PlayerTag))
        {
            gameManager.NextLevel();
        }
    }
}
