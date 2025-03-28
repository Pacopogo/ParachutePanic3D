using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperManager : MonoBehaviour
{
    [SerializeField] private Dropper[] droppers;

    [Header("Settings")]
    public float maxDrops = 2;                      //max amount of drops at the same time

    public static DropperManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DropRandomTrash()
    {
        int rnd = Random.Range(0, droppers.Length);

        droppers[rnd].DropTrash();
    }
}
