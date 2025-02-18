using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audioPlayer : MonoBehaviour
{
    [Header("Voice Lines")]
    [SerializeField] private VoiceLines voiceLines;             //list of voice lines or audio bites
    AudioSource audioSource;                                    //the audio player for the lines

    [Header("Background Audio")]
    [SerializeField] private AudioSource BackgroundPlayer;      //Background music component
    private bool isBGplaying;                                   //The toggle if it is allowed to play

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        isBGplaying = BackgroundPlayer.isPlaying;
    }

    //Call Voice line and play it
    public void playLine(int index)
    {
        StartCoroutine(CallAudioClip(index));
    }

    //It is IEnumerator for future expention so it will wait until its done talked until the next line
    private IEnumerator CallAudioClip(int i)
    {
        audioSource.clip = voiceLines.line[i];
        audioSource.Play();
        yield return new WaitForEndOfFrame();
    }

    //Function to Pause the background during the intro cutscene when terms and services are asked
    public void pauseBackgroundMusic()
    {
        if (isBGplaying)
        {
            BackgroundPlayer.Pause();
            isBGplaying = false;
            return;
        }
        BackgroundPlayer.UnPause();
        isBGplaying = true;

    }
}
