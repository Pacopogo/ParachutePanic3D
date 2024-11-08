using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionEvent : MonoBehaviour
{
    [SerializeField] private string objTag = "Player";
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnExit;

    //Unity event call when player or a assigned tag colides 
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(objTag))
            return;
        OnEnter.Invoke();
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag(objTag))
            return;
        OnExit.Invoke();
    }
}
