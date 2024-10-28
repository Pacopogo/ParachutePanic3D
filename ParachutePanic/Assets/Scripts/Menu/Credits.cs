using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public void sendToWebsite(string link)
    {
        System.Diagnostics.Process.Start(link);
    }
}
