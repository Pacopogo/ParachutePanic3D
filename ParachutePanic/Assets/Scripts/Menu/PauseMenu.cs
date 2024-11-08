using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseOBJ;   //The Object I toggle for the pause menu
    private bool isPaused = false;                  //Pause toggle check

    public void onPause(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        callPause();
    }

    //the function to toggle between paused and unPaused
    public void callPause()
    {
        isPaused = !isPaused;

        if (isPaused)                                   //Paused
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            pauseOBJ.SetActive(true);
        }
        else                                           //UnPaused
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            pauseOBJ.SetActive(false);

        }
    }
}
