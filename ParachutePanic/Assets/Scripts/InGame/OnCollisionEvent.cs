using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionEvent : MonoBehaviour
{
    [SerializeField] private string _objTag = "Trash";
    [SerializeField] private UnityEvent _onEnter;
    [SerializeField] private UnityEvent _onExit;

    //Unity event call when player or a assigned tag colides 
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(_objTag))
            return;

        _onEnter?.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag(_objTag))
            return;

        _onExit?.Invoke();
    }
}
