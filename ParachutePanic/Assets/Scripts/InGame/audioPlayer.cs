using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audioPlayer : MonoBehaviour
{
    [Header("Voice Lines")]
    [SerializeField] private VoiceLines voiceLines;
    AudioSource audioSource;

    [Header("Background Audio")]
    [SerializeField] private AudioSource BackgroundPlayer;
    private bool isBGplaying;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        isBGplaying = BackgroundPlayer.isPlaying;
    }

    public void playLine(int index)
    {
        StartCoroutine(CallAudioClip(index));
    }

    private IEnumerator CallAudioClip(int i)
    {
        //yield return new WaitUntil(() => audioSource.isPlaying == false);
        audioSource.clip = voiceLines.line[i];
        audioSource.Play();
        yield return new WaitForEndOfFrame();
    }

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
