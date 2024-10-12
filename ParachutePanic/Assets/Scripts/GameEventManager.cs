using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [SerializeField] private Dropper[] dropperList;

    private void Start()
    {
        CallDropper();
    }

    private void CallDropper()
    {
        Dropper dorpper = dropperList[Random.Range(0, dropperList.Length)];
        dorpper.StartCoroutine(dorpper.DropTrash(Random.Range(2, 30)));
        StartCoroutine(manageDrops());
    }

    private IEnumerator manageDrops()
    {
        yield return new WaitForSeconds(Random.Range(10,15));
        CallDropper();
        StopCoroutine(manageDrops());
    }
}
