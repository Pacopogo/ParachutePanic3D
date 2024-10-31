using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private playerSettings playerSettings;

    [Header("Audio")]
    [SerializeField] private Slider audioSlide;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Text audioText;

    private void Awake()
    {
        audioSlide.value = playerSettings.audioVolume;
    }
    private void Start()
    {
        changeGlobalAudio();
    }

    public void changeGlobalAudio()
    {
        audioMixer.SetFloat("game", audioSlide.value);
        playerSettings.audioVolume = audioSlide.value;
        audioText.text = (audioSlide.value + 80).ToString("f0") + "%";
    }
}
