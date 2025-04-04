using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject _pauseOBJ;
    private bool _isPaused = false;

    public void OnPause(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        CallPause();
    }

    public void CallPause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)                                   //Paused
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;

            _pauseOBJ.SetActive(true);
        }
        else                                           //UnPaused
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;

            _pauseOBJ.SetActive(false);

        }
    }
}
