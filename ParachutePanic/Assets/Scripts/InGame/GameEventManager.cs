using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//The Game event manager is the script that keeps track of all random events
//these are timed based random events

public class GameEventManager : MonoBehaviour
{

    [Header("Drop Settings")]
    [SerializeField] private int _maxDrops = 2;
    [SerializeField] private float _minDrop = 2;
    [SerializeField] private float _maxDrop = 8;

    private int _amountDropped;

    [Header("Button Settings")]
    [SerializeField] private float _minButton = 30;
    [SerializeField] private float _maxButton = 60;
    [SerializeField] private ButtonManager _buttonManager;

    [Header("Kart Settings")]
    [SerializeField] private float _minKart = 5;
    [SerializeField] private float _maxKart = 25;

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

    //Dropper
    private float _dummyDropTimer;
    private float _currentDropTime
    {
        get
        {
            return _dummyDropTimer;
        }
        set
        {
            _dummyDropTimer = value;
            if (_dummyDropTimer <= 0)
            {
                _dummyDropTimer = Random.Range(_minDrop,_maxDrop);
                Drop();
            }

            return;
        }
    }

    //Kart
    private float _dummyKartTimer;
    private float _currentKartTime
    {
        get
        {
            return _dummyKartTimer;
        }
        set
        {
            _dummyKartTimer = value;
            if (_dummyKartTimer <= 0)
            {
                _dummyKartTimer = Random.Range(_minKart, _maxKart);
                BreakKart(80);
            }

            return;
        }
    }

    #endregion

    private void Start()
    {
        _isPlaying = true;

        _amountDropped = 0;

        //initial timers
        _currentDropTime    = 5;
        _currentButtonTime  = 25;
        _currentKartTime    = 15;
    }

    private void Update()
    {
        if (!_isPlaying)
            return;

        _currentDropTime     -= 1 * Time.deltaTime;
        _currentButtonTime   -= 1 * Time.deltaTime;
        _currentKartTime     -= 1 * Time.deltaTime;
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
        Objectpool.Instance.ClearObjects();
    }

    #region Dropper logic

    private void Drop()
    {
        if (_amountDropped < _maxDrops)
        {
            DropperManager.instance.DropRandomTrash();
            SetAmountDropped(1);
        }
    }
    public void SetAmountDropped(int amount)
    {
        _amountDropped += amount;

        if (_amountDropped < 0)
            _amountDropped = 0;
    }

    #endregion

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

    #region Kart Logic

    private void BreakKart(float chance)
    {
        chance = Mathf.Clamp(chance, 0, 100);
        
        float rnd = Random.Range(0, 100);

        if (rnd < chance)
        {
            PlayKartSound();
            KartManager.instance.BreakRandomKart();
        }
    }

    private void PlayKartSound()
    {
        _audioSource.pitch = 1;
        _audioSource.clip = _clipList[1];
        _audioSource.Play();
    }

    #endregion

}
