using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("�¼�����")]
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;

    [Header("���")]
    public AudioSource BGMSource;
    public AudioSource FXSource;

    private void OnEnable()
    {
        //FXEvent.OnEventRaised += OnFXEvent;
        BGMEvent.OnEventRaised += OnBGMEvent;
    }

    private void OnDisable()
    {
        //FXEvent.OnEventRaised -= OnFXEvent;
        BGMEvent.OnEventRaised -= OnBGMEvent;
    }

    private void OnBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    /*    private void OnFXEvent(AudioClip clip)
        {
            FXSource.clip = clip;
            FXSource.Play();
        }*/
}
