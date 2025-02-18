using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private playerSettings playerSettings;     //The global player settings

    [Header("Audio")]
    [SerializeField] private Slider audioSlide;                 //UI element slider
    [SerializeField] private AudioMixer audioMixer;             //The Mixer for all audio
    [SerializeField] private TMP_Text audioText;                //UI Text element

    private void Start()
    {
        audioSlide.value = playerSettings.audioVolume;                  //Set the slider to the correct audio
        audioMixer.SetFloat("game", audioSlide.value);                  //Set tha audio mixer correct
        audioText.text = (audioSlide.value + 80).ToString("f0") + "%";  //change the text element
        //The audio mixer goes as low as -80 so I do +80 to display it as 0 instead as the lowest volume
    }

    //change the audio on slider input
    public void changeGlobalAudio()
    {
        audioMixer.SetFloat("game", audioSlide.value);
        playerSettings.audioVolume = audioSlide.value;                  //Changes the global audio setting
        audioText.text = (audioSlide.value + 80).ToString("f0") + "%";
    }
}
