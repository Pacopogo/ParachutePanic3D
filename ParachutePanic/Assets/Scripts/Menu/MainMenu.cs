using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

[RequireComponent(typeof(SceneLoader))]
public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Toggle introToggle;

    [Header("Mouse")]
    [SerializeField] private playerSettings playerSettings;
    [SerializeField] private Slider sensSlider;
    [SerializeField] private TMP_Text mouseSensText;

    [Header("Audio")]
    [SerializeField] private Slider audioSlide;
    [SerializeField]private AudioMixer audioMixer;
    [SerializeField] private TMP_Text audioText;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        sceneLoader = GetComponent<SceneLoader>();
        sensSlider.value = playerSettings.mouseSensitivity;
        changeGlobalAudio();
    }

    public void PlayGame()
    {
        if (introToggle.isOn)
        {
            sceneLoader.LoadSceneString("Intro");
        }
        else
        {
            sceneLoader.LoadSceneString("GameScene");
        }
    }

    public void changeMouseSens()
    {
        playerSettings.mouseSensitivity = sensSlider.value;
        mouseSensText.text = sensSlider.value.ToString("f1");
    }

    public void changeGlobalAudio()
    {
        audioMixer.SetFloat("game", audioSlide.value);
        audioText.text = (audioSlide.value + 80).ToString("f0") + "%";
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
