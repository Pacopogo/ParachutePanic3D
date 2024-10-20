using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audioPlayer : MonoBehaviour
{
    [SerializeField] private VoiceLines voiceLines;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
}
