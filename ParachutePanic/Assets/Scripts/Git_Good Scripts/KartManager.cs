using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KartManager : MonoBehaviour
{
    [SerializeField] private Kart[] karts;

    public static KartManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void BreakRandomKart()
    {
        int rnd = Random.Range(0, karts.Length);
        karts[rnd].toggleActiveKart(false);
    }
    public void BreakKartIndex(int index) => karts[index].toggleActiveKart(false);

    public void RepairAllKarts()
    {
        for (int i = 0; i < karts.Length; i++)
        {
            karts[i].toggleActiveKart(true);
        }
    }

}
