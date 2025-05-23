using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

[RequireComponent(typeof(SceneLoader))]
public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;           //Screne loader to be able to load other scenes
    [SerializeField] private Toggle introToggle;                //UI toggle element

    [Header("Mouse")]
    [SerializeField] private playerSettings playerSettings;     //global player settings
    [SerializeField] private Slider sensSlider;                 //UI Slider Element
    [SerializeField] private TMP_Text mouseSensText;            //UI Text Element

    private void Start()
    {
        //set time and cursor back
        Time.timeScale      = 1;
        Cursor.lockState    = CursorLockMode.None;

        //Get the Sceneloader component incase it isn't there
        sceneLoader         = GetComponent<SceneLoader>();
        //set slider value to the global sensitivity value
        sensSlider.value    = playerSettings.mouseSensitivity;
    }

    public void PlayGame()
    {
        //the if statement to see if you want the intro or not
        if (introToggle.isOn)
        {
            sceneLoader.LoadSceneString("Intro");
        }
        else
        {
            sceneLoader.LoadSceneString("GameScene");
        }
    }

    //Slider event to change the mouse sens and update its text
    public void changeMouseSens()
    {
        playerSettings.mouseSensitivity = sensSlider.value;
        mouseSensText.text = sensSlider.value.ToString("f1");
    }

    //Basic Game exit button event
    public void ExitGame()
    {
        Application.Quit();
    }
}
