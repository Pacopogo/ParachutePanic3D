using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//The Game event manager is the script that keeps track of all random events
//these are timed based random events

public class GameEventManager : MonoBehaviour
{
    public void EndGame()
    {
        Objectpool.instance.ClearObjects();
        gameObject.SetActive(false);
        return;
    }

}
