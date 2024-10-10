using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class PacoButton : MonoBehaviour
{
    enum ButtonState
    {
        On,
        Pressed,
        Off,
    }

    [SerializeField] private ButtonState state;

    [SerializeField] private Material[] buttonMaterial = new Material[3];
    [SerializeField] private UnityEvent onClick;
    [SerializeField] private UnityEvent onRelease;

    private MeshRenderer buttonMesh;
    private bool toggleActive;

    private void Start()
    {
        buttonMesh = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        switch (state)
        {
            case ButtonState.On:
                buttonMesh.material = buttonMaterial[0];
                break;
            case ButtonState.Pressed:
                buttonMesh.material = buttonMaterial[1];

                break;
            case ButtonState.Off:
                buttonMesh.material = buttonMaterial[2];

                break;
            default:
                break;
        }
    }

    public void OnPressed()
    {
        if (state == ButtonState.Off)
            return;

        state = ButtonState.Pressed;
        onClick.Invoke();
    }

    public void OnRelease()
    {
        if (state == ButtonState.Off)
            return;

        onRelease.Invoke();
        state = ButtonState.On;
    }

    public void toggleButton(bool active)
    {
        if (active)
        {
            state = ButtonState.On;
        }
        else
        {
            state = ButtonState.Off;
        }
    }
}
