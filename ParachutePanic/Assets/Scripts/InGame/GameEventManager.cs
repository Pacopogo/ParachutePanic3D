using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [Header("Drop Settings")]
    public int MaxTrashAmount = 1;
    [SerializeField] private Dropper[] dropperList;
    [SerializeField] private float minDrop, maxDrop;
    public List<GameObject> trashObj;

    [Header("Button Settings")]
    [SerializeField] private PacoButton[] buttonList;
    [SerializeField] private float minButton, maxButton;

    [Header("Kart Settings")]
    [SerializeField] private Kart[] kartList;
    [SerializeField] private float minKart, maxKart;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip[] clipList;
    private AudioSource audioSource;

    private bool isPlaying;

    private void Start()
    {
        isPlaying = true;
        audioSource = GetComponent<AudioSource>();

        CallDropper(); 
        StartCoroutine(RandomButton(15));
        callRandomKartDisable();
    }
    
    public void endGame()
    {
        isPlaying = false;
        return;
    }
    private void FixedUpdate()
    {
        clearTrashList();
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

        Dropper dorpper = dropperList[Random.Range(0, dropperList.Length)];
        dorpper.DropTrash();
        StartCoroutine(manageDrops());
    }

    private IEnumerator manageDrops()
    {
        //clearTrashList();
        yield return new WaitForSeconds(Random.Range(minDrop, maxDrop));
        CallDropper();
        StopCoroutine(manageDrops());
    }

    public void clearTrashList()
    {
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
        yield return new WaitForSeconds(intial);
        yield return new WaitForSeconds(Random.Range(minButton, maxButton));

        for (int i = 0; i < Random.Range(2, buttonList.Length); i++)
        {
            yield return new WaitForSeconds(0.3f);
            buttonList[Random.Range(0, buttonList.Length)].toggleButton(false);
            
            audioSource.clip = clipList[0];
            audioSource.pitch = 2;
            audioSource.Play();
        }
        toggleRandomButton();
        StopCoroutine(RandomButton(0));
    }

    public void callRandomKartDisable()
    {
        StartCoroutine(RandomKartDisable());
    }
    private IEnumerator RandomKartDisable()
    {
        yield return new WaitForSeconds(Random.Range(minKart, maxKart));

        float rnd = Random.Range(0, 100);
        if (rnd >= 80)
        {
            audioSource.clip = clipList[1];
            audioSource.pitch = 1;
            audioSource.Play();
            kartList[Random.Range(0, kartList.Length)].toggleActiveKart(false);
        }

        callRandomKartDisable();
        StopCoroutine(RandomKartDisable());
        
    }
}
