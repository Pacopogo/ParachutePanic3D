using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperManager : MonoBehaviour, IManager
{
    [SerializeField] private Dropper[] _droppers;

    [Header("Settings")]
    [SerializeField] private float _startTime = 5;
    [SerializeField] private Vector2 _timerRange = new Vector2(2, 5);
    [SerializeField] private float _maxDrops = 2;                       //max amount of drops at the same time

    private void Start()
    {
        StartCoroutine(DropTrash(_startTime));
    }

    public void DropRandomTrash()
    {
        int rnd = Random.Range(0, _droppers.Length);

        _droppers[rnd].DropTrash();
    }

    private IEnumerator DropTrash(float waitTime)
    {
        Debug.Log("Dropping");
        int amount = Random.Range(1, _droppers.Length);
        float time = Random.Range(_timerRange.x, _timerRange.y);

        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(1f);
            DropRandomTrash();
        }

        StartCoroutine(DropTrash(time));

        StopCoroutine(DropTrash(waitTime));
    }
}
