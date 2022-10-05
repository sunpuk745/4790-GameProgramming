using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private LevelDoorAudioController levelDoorAudioController;

    private bool winningSoundIsPlaying = false;

    private const string PlayerTag = "Player";

    private void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();    
    }

    private IEnumerator FinishWinningSoundBeforeChangeScene()
    {
        winningSoundIsPlaying = true;
        levelDoorAudioController.PlayWinningSound();
        yield return new WaitForSeconds(0.6f);
        if (gameManager == null)
            {
                gameManager = FindObjectOfType<GameManager>();
            }
        gameManager.NextLevel();
        winningSoundIsPlaying = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag) && !winningSoundIsPlaying)
        {
            StartCoroutine(FinishWinningSoundBeforeChangeScene());
        }
    }
}
