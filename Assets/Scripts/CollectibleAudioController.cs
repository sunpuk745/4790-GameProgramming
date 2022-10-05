using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SOAudioScript collectedAudioClips;
    [SerializeField] private SOAudioScript respawnedAudioClips;

    public void PlayCollectedSound()
    {
        audioSource.PlayOneShot(collectedAudioClips.GetAudioClip(), 0.5f);
    }

    public void PlayRespawnedSound()
    {
        audioSource.PlayOneShot(respawnedAudioClips.GetAudioClip(), 0.5f);
    }
}
