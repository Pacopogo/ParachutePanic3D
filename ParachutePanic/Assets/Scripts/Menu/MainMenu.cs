using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SceneLoader))]
public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Toggle introToggle;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        sceneLoader = GetComponent<SceneLoader>();
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

    public void ExitGame()
    {
        Application.Quit();
    }
}
