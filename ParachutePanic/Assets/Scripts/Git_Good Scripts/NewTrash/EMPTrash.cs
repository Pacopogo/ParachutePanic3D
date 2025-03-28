using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPTrash : MonoBehaviour
{
    public void onEmp() => ButtonManager.instance.DisableRandomButton(); 
}
