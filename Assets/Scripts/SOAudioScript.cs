using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Create AudioClips", fileName = "New AudioClips")]

public class SOAudioScript : ScriptableObject
{
    [SerializeField] private AudioClip[] audioClips;

    public AudioClip GetAudioClip()
    {
        var totalAudioClips = audioClips.Length;
        int index;

        switch (totalAudioClips)
        {
            case 1:
                index = 0;
                break;
            default:
                index = Random.Range(0, totalAudioClips);
                break;
        }

        return audioClips[index];
    }
}
