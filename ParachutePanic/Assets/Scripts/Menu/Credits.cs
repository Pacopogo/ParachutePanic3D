using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    //Button event that sends you to the linked website
    public void SendToWebsite(string link)
    {
        System.Diagnostics.Process.Start(link);
    }
}
