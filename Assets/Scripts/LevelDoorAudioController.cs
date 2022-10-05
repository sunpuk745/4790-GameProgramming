using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SOAudioScript winningAudioClips;

    public void PlayWinningSound()
    {
        audioSource.PlayOneShot(winningAudioClips.GetAudioClip(), 0.2f);
    }
}
