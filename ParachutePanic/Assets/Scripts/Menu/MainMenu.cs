using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

[RequireComponent(typeof(SceneLoader))]
public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Toggle _introToggle;

    [Header("Mouse")]
    [SerializeField] private playerSettings _playerSettings;
    [SerializeField] private Slider _sensSlider;
    [SerializeField] private TMP_Text _mouseSensText;

    private void Start()
    {
        //set time and cursor back
        Time.timeScale      = 1;
        Cursor.lockState    = CursorLockMode.None;

        //Get the Sceneloader component incase it isn't there
        _sceneLoader         = GetComponent<SceneLoader>();
        //set slider value to the global sensitivity value
        _sensSlider.value    = _playerSettings.MouseSensitivity;
    }

    public void PlayGame()
    {
        //the if statement to see if you want the intro or not
        if (_introToggle.isOn)
        {
            _sceneLoader.LoadSceneString("Intro");
        }
        else
        {
            _sceneLoader.LoadSceneString("GameScene");
        }
    }

    //Slider event to change the mouse sens and update its text
    public void ChangeMouseSens()
    {
        _playerSettings.MouseSensitivity = _sensSlider.value;
        _mouseSensText.text = _sensSlider.value.ToString("f1");
    }

    //Basic Game exit button event
    public void ExitGame()
    {
        Application.Quit();
    }
}
