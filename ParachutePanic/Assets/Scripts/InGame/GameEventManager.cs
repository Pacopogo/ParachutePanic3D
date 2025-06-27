using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//The Game event manager is the script that keeps track of all random events
//these are timed based random events

public class GameEventManager : MonoBehaviour
{

    [Header("Button Settings")]
    [SerializeField] private float _minButton = 30;
    [SerializeField] private float _maxButton = 60;
    [SerializeField] private ButtonManager _buttonManager;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip[] _clipList;
    [SerializeField] private AudioSource _audioSource;

    private bool _isPlaying;

    #region Getters & Setters
    
    //Buttons
    private float _dummyButtonTimer;
    private float _currentButtonTime
    {
        get
        {
            return _dummyButtonTimer;
        }
        set
        {
            _dummyButtonTimer = value;
            if (_dummyButtonTimer <= 0)
            {
                _dummyButtonTimer = Random.Range(_minButton, _maxButton);
                ToggleButton();
            }

            return;
        }
    }


    #endregion

    private void Start()
    {
        _isPlaying = true;


        //initial timers
        _currentButtonTime  = 25;
    }

    private void Update()
    {
        if (!_isPlaying)
            return;

        _currentButtonTime   -= 1 * Time.deltaTime;
    }

    //Set the game stop when the game is over
    //This is so all the other fucntions don't continue unnesaccerly
    public void EndGame()
    {
        _isPlaying = false;

        ClearGame();
        return;
    }

    //to clear the game of Trash objects
    public void ClearGame()
    {
        Objectpool.instance.ClearObjects();
    }

    #region Button Disabler

    private void ToggleButton()
    {
        Debug.Log("BUTTON HIT");
        //pick between either 2 or all buttons to disable
        int rnd = Random.Range(2, _buttonManager.Buttons.Length);

        for (int i = 0; i < rnd; i++)
        {
            ButtonManager.instance.DisableRandomButton();
        }
        PlayButtonSound();
    }

    private void PlayButtonSound()
    {
        _audioSource.clip = _clipList[0];
        _audioSource.pitch = 2;
        _audioSource.Play();
    }

    #endregion

}
