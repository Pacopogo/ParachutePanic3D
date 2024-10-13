using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [SerializeField] private Dropper[] dropperList;
    [SerializeField] private PacoButton[] buttonList;
    [SerializeField] private Kart[] kartList;

    [SerializeField] private AudioClip[] clipList;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        CallDropper(); 
        StartCoroutine(RandomButton(15));
        callRandomKartDisable();
    }

    private void CallDropper()
    {
        Dropper dorpper = dropperList[Random.Range(0, dropperList.Length)];
        dorpper.StartCoroutine(dorpper.DropTrash(Random.Range(5, 30)));
        StartCoroutine(manageDrops());
    }

    private IEnumerator manageDrops()
    {
        yield return new WaitForSeconds(Random.Range(10,15));
        CallDropper();
        StopCoroutine(manageDrops());
    }

    public void toggleRandomButton()
    {
        StartCoroutine(RandomButton(0));
    }
    private IEnumerator RandomButton(float intial)
    {
        yield return new WaitForSeconds(intial);
        yield return new WaitForSeconds(Random.Range(30, 60));

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
        yield return new WaitForSeconds(Random.Range(10, 30));

        float rnd = Random.Range(0, 100);
        if (rnd >= 50)
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
