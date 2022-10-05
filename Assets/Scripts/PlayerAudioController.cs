using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SOAudioScript walkAudioClips;
    [SerializeField] private SOAudioScript jumpAudioClips;
    [SerializeField] private SOAudioScript fallOnImpactAudioClips;
    [SerializeField] private SOAudioScript deathAudioClips;

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpAudioClips.GetAudioClip(), 0.5f);
    }

    public void PlayWalkSound()
    {
        audioSource.PlayOneShot(walkAudioClips.GetAudioClip(), 0.4f);
    }

    public void PlayFallOnImpact()
    {
        audioSource.PlayOneShot(fallOnImpactAudioClips.GetAudioClip(), 0.8f);
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathAudioClips.GetAudioClip());
    }
}
