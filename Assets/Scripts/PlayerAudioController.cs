using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Start() 
    {

    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
