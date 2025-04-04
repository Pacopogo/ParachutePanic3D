using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private playerSettings _playerSettings;

    [Header("Audio")]
    [SerializeField] private Slider _audioSlide;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Text _audioText;

    private void Start()
    {
        _audioSlide.value = _playerSettings.AudioVolume;
        _audioMixer.SetFloat("game", _audioSlide.value);
        
        //The audio mixer goes as low as -80 so I do +80 to display it as 0 instead as the lowest volume
        _audioText.text = (_audioSlide.value + 80).ToString("f0") + "%";
    }
    public void ChangeGlobalAudio()
    {
        _audioMixer.SetFloat("game", _audioSlide.value);
        _playerSettings.AudioVolume = _audioSlide.value;

        _audioText.text = (_audioSlide.value + 80).ToString("f0") + "%";
    }
}
