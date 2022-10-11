using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerAudioController playerAudioController;
    [SerializeField] PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Ground" && playerController.deathSoundIsPlaying == false)
        {
            playerAudioController.PlayFallOnImpact();
        }
    }
}
