using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperManager : MonoBehaviour
{
    [SerializeField] private Dropper[] droppers;

    //[SerializeField] private 

    [Header("Settings")]
    public float maxDrops = 2;                      //max amount of drops at the same time

    public void DropRandomTrash()
    {

    }
}
