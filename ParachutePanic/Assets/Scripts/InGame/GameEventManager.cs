using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//The Game event manager is the script that keeps track of all random events
//these are timed based random events

//Note for future pablo: Ask Peers and/or teachers how to refactor this so there aren't Coroutines 

public class GameEventManager : MonoBehaviour
{

    [Header("Drop Settings")]
    [SerializeField] private int maxDrops = 2;                  //Amount of Trash that is allowed to drop
    [SerializeField] private DropperManager dropperManager;
    [SerializeField] private float minDrop;                     //Minimum seconds between drops
    [SerializeField] private float maxDrop;                     //Maximum seconds between drops

    private float dummyDropTimer;
    private float currentDropTime
    {
        get
        {
            return dummyDropTimer;
        }
        set
        {
            dummyDropTimer = value;
            if (dummyDropTimer <= 0)
            {
                dummyDropTimer = Random.Range(minDrop,maxDrop);
                Drop();
            }

            return;
        }
    }

    private int amountDropped;

    [Header("Button Settings")]
    [SerializeField] private float minButton;                   //Minimum seconds between them breaking
    [SerializeField] private float maxButton;                   //Max seconds between them breaking
    [SerializeField] private ButtonManager buttonManager;

    private float dummyButtonTimer;
    private float currentButtonTime
    {
        get
        {
            return dummyButtonTimer;
        }
        set
        {
            dummyButtonTimer = value;
            if (dummyButtonTimer <= 0)
            {
                dummyButtonTimer = Random.Range(minButton, maxButton);
                ToggleButton();
            }

            return;
        }
    }

    [Header("Kart Settings")]
    [SerializeField] private float minKart;                     //Minimum seconds between kart breaking
    [SerializeField] private float maxKart;                     //Maxium seconds between kart breaking

    private float dummyKartTimer;
    private float currentKartTime
    {
        get
        {
            return dummyKartTimer;
        }
        set
        {
            dummyKartTimer = value;
            if (dummyKartTimer <= 0)
            {
                dummyKartTimer = Random.Range(minKart, maxKart);
                BreakKart();
            }

            return;
        }
    }

    [Header("Audio Settings")]
    [SerializeField] private AudioClip[] clipList;              //Audio clips to play diffrent sound effects
    [SerializeField] private AudioSource audioSource;           //Audio component to play the clips from

    private bool isPlaying;                                     //Is the game playing

    private void Start()
    {
        isPlaying = true;

        amountDropped = 0;

        //initial timers
        currentDropTime     = 5;
        currentButtonTime   = 25;
        currentKartTime     = 15;
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        currentDropTime     -= 1 * Time.deltaTime;
        currentButtonTime   -= 1 * Time.deltaTime;
        currentKartTime     -= 1 * Time.deltaTime;
    }
    //Set the game stop when the game is over
    //This is so all the other fucntions don't continue unnesaccerly
    public void EndGame()
    {
        isPlaying = false;
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
        if (amountDropped < maxDrops)
        {
            DropperManager.Instance.DropRandomTrash();
            SetAmountDropped(1);
        }

    }
    public void SetAmountDropped(int amount)
    {
        amountDropped += amount;

        if (amountDropped < 0)
            amountDropped = 0;
    }

    #endregion

    #region Button Disabler

    private void ToggleButton()
    {
        Debug.Log("BUTTON HIT");
        //pick between either 2 or all buttons to disable
        int rnd = Random.Range(2, buttonManager.Buttons.Length);

        for (int i = 0; i < rnd; i++)
        {
            ButtonManager.instance.DisableRandomButton();
        }
        PlayButtonSound();
    }

    private void PlayButtonSound()
    {
        audioSource.clip = clipList[0];
        audioSource.pitch = 2;
        audioSource.Play();
    }

    #endregion

    #region Kart Logic

    private void BreakKart()
    {
        //20% chance to break everytime it is called
        float rnd = Random.Range(0, 5);
        if (rnd == 4)
        {
            PlayKartSound();
            KartManager.instance.BreakRandomKart();
        }
    }

    private void PlayKartSound()
    {
        audioSource.pitch = 1;
        audioSource.clip = clipList[1];
        audioSource.Play();
    }

    #endregion

}
