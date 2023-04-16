using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ¹ÒÔÚµ½¹¥»÷ÉÏ
/// </summary>
public class AudioDefination : MonoBehaviour
{
    public PlayAudioEventSO playAudioEvent;
    public AudioClip audioClip;
    public bool playOnEnable;

    private void OnEnable()
    {
        if (playOnEnable)
            PlayAudioClip();
    }

    public void PlayAudioClip()
    {
        playAudioEvent.RaiseEvent(audioClip);
    }
}
