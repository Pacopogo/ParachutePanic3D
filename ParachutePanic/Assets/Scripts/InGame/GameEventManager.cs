using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Game event manager is the script that keeps track of all random events
//these are timed based random events

//Note for future pablo: Ask Peers and/or teachers how to refactor this so there aren't Coroutines 

public class GameEventManager : MonoBehaviour
{

    [Header("Drop Settings")]
    [SerializeField] private int maxDrops = 2;                  //Amount of Trash that is allowed to drop
    [SerializeField] private Dropper[] dropperList;             //Array of all Droppers
    [SerializeField] private float minDrop, maxDrop;            //The minimum and maximum seconds between drops
    
    private int amountDropped;

    [Header("=========================================")]
    [Header("Button Settings")]
    [SerializeField] private PacoButton[] buttonList;           //Array of all button
    [SerializeField] private float minButton, maxButton;        //Min and Max Seconds between them breaking

    [Header("=========================================")]
    [Header("Kart Settings")]
    [SerializeField] private Kart[] kartList;                   //Array of all Karts
    [SerializeField] private float minKart, maxKart;            //Min and Max Seconds between them breaking


    [Header("=========================================")]
    [Header("Audio Settings")]
    [SerializeField] private AudioClip[] clipList;              //Audio clips to play diffrent sound effects
    [SerializeField] private AudioSource audioSource;           //Audio component to play the clips from

    private bool isPlaying;                                     //Is the game playing

    private void Start()
    {
        this.isPlaying = true;

        this.amountDropped = 0;

        //Initial call for all the event
        CallDropper();
        StartCoroutine(RandomButton(15));           //15 second inital cooldown
        callRandomKartDisable();
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
    private void CallDropper()
    {
        if (!isPlaying)
            return;

        //take a random dropper and assign it to the variable
        StartCoroutine(manageDrops());
    }

    private IEnumerator manageDrops()
    {
        //waiting random amount of seconds between te min and max range
        yield return new WaitForSeconds(Random.Range(this.minDrop, this.maxDrop));

        //drop from random dropper
        if (this.amountDropped < maxDrops)
        {
            GetRandomDropper().DropTrash();
            SetAmountDropped(1);
        }

        //recall to loop
        CallDropper();
        StopCoroutine(manageDrops());
    }
    private Dropper GetRandomDropper()
    {
        Dropper dorpper = this.dropperList[Random.Range(0, this.dropperList.Length)];
        return dorpper;
    }

    public void SetAmountDropped(int amount)
    {
        this.amountDropped += amount;

        if (this.amountDropped < 0)
            this.amountDropped = 0;
    }

    #endregion

    #region Button Disabler
    public void ToggleRandomButton()
    {
        StartCoroutine(RandomButton(0));
    }
    private IEnumerator RandomButton(float interval)
    {
        //wait the inital time + a random between the second range
        yield return new WaitForSeconds(interval);
        yield return new WaitForSeconds(Random.Range(minButton, maxButton));

        //pick between either 2 or all buttons to disable
        for (int i = 0; i < Random.Range(2, this.buttonList.Length); i++)
        {
            //0.3 second interval between each disabled button
            yield return new WaitForSeconds(0.3f);
            GetRandomButton().toggleButton(false);

            PlayButtonSound();
        }

        //Recall the script to loop
        ToggleRandomButton();
        StopCoroutine(RandomButton(0));
    }

    private void PlayButtonSound()
    {
        this.audioSource.clip = clipList[0];
        this.audioSource.pitch = 2;
        this.audioSource.Play();
    }

    private PacoButton GetRandomButton()
    {
        PacoButton button = this.buttonList[Random.Range(0, this.buttonList.Length)];
        return button;
    }
    #endregion

    #region Kart Logic
    public void callRandomKartDisable()
    {
        StartCoroutine(RandomKartDisable());
    }
    private IEnumerator RandomKartDisable()
    {
        //Waiting random seconds between 
        yield return new WaitForSeconds(Random.Range(minKart, maxKart));

        //20% chance to break everytime it is called
        float rnd = Random.Range(0, 5);
        if (rnd == 4)
        {
            PlayKartSound();
            GetRandomKart().toggleActiveKart(false);
        }

        //recall function to loop
        callRandomKartDisable();
        StopCoroutine(RandomKartDisable());

    }

    private void PlayKartSound()
    {
        this.audioSource.pitch = 1;
        this.audioSource.clip = clipList[1];
        this.audioSource.Play();
    }

    private Kart GetRandomKart()
    {
        Kart rmdKart = this.kartList[Random.Range(0, this.kartList.Length)];
        return rmdKart;
    }
    #endregion
}
