using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [Header("Drop Settings")]
    public int MaxTrashAmount = 1;
    [SerializeField] private Dropper[] dropperList;             //Array of all Droppers
    [SerializeField] private float minDrop, maxDrop;            //The minimum and maximum seconds between drops
    public List<GameObject> trashObj;                           //List of Trash that is in scene

    [Header("Button Settings")]
    [SerializeField] private PacoButton[] buttonList;           //Array of all button
    [SerializeField] private float minButton, maxButton;        //Min and Max Seconds between them breaking

    [Header("Kart Settings")]
    [SerializeField] private Kart[] kartList;                   //Array of all Karts
    [SerializeField] private float minKart, maxKart;            //Min and Max Seconds between them breaking

    [Header("Audio Settings")]
    [SerializeField] private AudioClip[] clipList;              //Audio clips to play diffrent sound effects
    private AudioSource audioSource;                            //Audio component to play the clips from

    private bool isPlaying;                                     //Is the game playing

    private void Start()
    {
        isPlaying = true;
        audioSource = GetComponent<AudioSource>();

        //Initial call for all the event
        CallDropper(); 
        StartCoroutine(RandomButton(15));       //15 second inital cooldown so you don't get thown into the deep end
        callRandomKartDisable();
    }
    
    //Set the game stop when the game is over
    //This is so all the other fucntions don't continue unnesaccerly
    public void endGame()
    {
        isPlaying = false;
        return;
    }
    private void FixedUpdate()
    {
        clearTrashList();                           //Clear the list and Objects
        
        //once the game is over delete all the trash still falling to not further the gameplay
        if (isPlaying)                              
            return;

        for (int i = 0; i < trashObj.Count; i++)
        {
            Destroy(trashObj[i]);
        }
    }

    private void CallDropper()
    {
        if (!isPlaying)
            return;
        //take a random dropper and assign it to the variable
        Dropper dorpper = dropperList[Random.Range(0, dropperList.Length)];
        dorpper.DropTrash();
        StartCoroutine(manageDrops());
    }

    private IEnumerator manageDrops()
    {
        //waiting random amount of seconds between te min and max range
        yield return new WaitForSeconds(Random.Range(minDrop, maxDrop));

        //recall to loop
        CallDropper();
        StopCoroutine(manageDrops());
    }

    public void clearTrashList()
    {
        //if the list is bigger then 0 then go throught the list and remove the empty spots
        if (trashObj.Count > 0)
        {
            for (int i = trashObj.Count - 1; i >= 0; i--)
            {
                if (trashObj[i] == null)
                {
                    trashObj.Remove(trashObj[i]);
                }
            }
        }
    }
    public void toggleRandomButton()
    {
        StartCoroutine(RandomButton(0));
    }
    private IEnumerator RandomButton(float intial)
    {
        //wait the inital time + a random between the second range
        yield return new WaitForSeconds(intial);
        yield return new WaitForSeconds(Random.Range(minButton, maxButton));

        //pick between either 2 or all buttons to disable
        for (int i = 0; i < Random.Range(2, buttonList.Length); i++)
        {
            //wait shortly between each disable
            yield return new WaitForSeconds(0.3f);
            buttonList[Random.Range(0, buttonList.Length)].toggleButton(false);
            
            //Manipulate audio pitch to lessen the diffrent clips used
            audioSource.clip = clipList[0];
            audioSource.pitch = 2;
            audioSource.Play();
        }

        //Recall the script to loop
        toggleRandomButton();
        StopCoroutine(RandomButton(0));
    }

    public void callRandomKartDisable()
    {
        StartCoroutine(RandomKartDisable());
    }
    private IEnumerator RandomKartDisable()
    {
        //Waiting random seconds between 
        yield return new WaitForSeconds(Random.Range(minKart, maxKart));

        //Make a random number between 0 and a 100 and if it is above or 80 it will disable a kart
        //In otherwords there is a 20% chance to break everytime it is called
        float rnd = Random.Range(0, 100);
        if (rnd >= 80)
        {
            audioSource.clip = clipList[1];
            audioSource.pitch = 1;
            audioSource.Play();
            kartList[Random.Range(0, kartList.Length)].toggleActiveKart(false); //take a random kart from the array
        }

        //recall function to loop
        callRandomKartDisable();
        StopCoroutine(RandomKartDisable());
        
    }
}
