using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperManager : MonoBehaviour
{
    [SerializeField] private Dropper[] _droppers;

    [Header("Settings")]
    public float MaxDrops = 2;                      //max amount of drops at the same time

    public static DropperManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DropRandomTrash()
    {
        int rnd = Random.Range(0, _droppers.Length);

        _droppers[rnd].DropTrash();
    }
}
