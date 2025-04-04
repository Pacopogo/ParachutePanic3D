using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KartManager : MonoBehaviour
{
    [SerializeField] private Kart[] _karts;

    public static KartManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void BreakRandomKart()
    {
        int rnd = Random.Range(0, _karts.Length);
        _karts[rnd].ToggleActiveKart(false);
    }
    public void BreakKartIndex(int index) => _karts[index].ToggleActiveKart(false);

    public void RepairAllKarts()
    {
        for (int i = 0; i < _karts.Length; i++)
        {
            _karts[i].ToggleActiveKart(true);
        }
    }

}
