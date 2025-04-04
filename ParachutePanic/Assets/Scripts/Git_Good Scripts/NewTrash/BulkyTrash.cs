using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulkyTrash : MonoBehaviour
{
    public void OnBulkyHit() => KartManager.instance.BreakRandomKart();
}
