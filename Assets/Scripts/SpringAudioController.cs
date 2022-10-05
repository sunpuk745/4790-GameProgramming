using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SOAudioScript sprungAudioClips;

    public void PlaySprungSound()
    {
        audioSource.PlayOneShot(sprungAudioClips.GetAudioClip(), 0.5f);
    }
}
