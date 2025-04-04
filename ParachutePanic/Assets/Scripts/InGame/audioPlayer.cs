using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audioPlayer : MonoBehaviour
{
    [Header("Voice Lines")]
    [SerializeField] private VoiceLines _voiceLines;             //list of voice lines or audio bites
    private AudioSource _audioSource;                                    //the audio player for the lines

    [Header("Background Audio")]
    [SerializeField] private AudioSource _backgroundPlayer;      //Background music component
    private bool _isBGplaying;                                   //The toggle if it is allowed to play

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _isBGplaying = _backgroundPlayer.isPlaying;
    }

    //Call Voice line and play it
    public void PlayLine(int index)
    {
        StartCoroutine(CallAudioClip(index));
    }

    //It is IEnumerator for future expention so it will wait until its done talked until the next line
    private IEnumerator CallAudioClip(int i)
    {
        _audioSource.clip = _voiceLines.line[i];
        _audioSource.Play();
        yield return new WaitForEndOfFrame();
    }

    //Function to Pause the background during the intro cutscene when terms and services are asked
    public void PauseBackgroundMusic()
    {
        if (_isBGplaying)
        {
            _backgroundPlayer.Pause();
            _isBGplaying = false;
            return;
        }
        _backgroundPlayer.UnPause();
        _isBGplaying = true;

    }
}
