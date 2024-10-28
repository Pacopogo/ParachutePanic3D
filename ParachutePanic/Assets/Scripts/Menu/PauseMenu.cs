using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseOBJ;
    private bool isPaused = false;

    public void onPause(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        callPause();
    }

    public void callPause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            pauseOBJ.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            pauseOBJ.SetActive(false);

        }
    }
}
