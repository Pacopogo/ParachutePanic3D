using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        sceneLoader = GetComponent<SceneLoader>();
        sensSlider.value = playerSettings.mouseSensitivity;

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

    public void ExitGame()
    {
        Application.Quit();
    }
}
