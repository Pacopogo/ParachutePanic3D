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

    [SerializeField] private ButtonState _state;

    [SerializeField] private Material[] _buttonMaterial = new Material[3];
    [SerializeField] private UnityEvent _onClick;
    [SerializeField] private UnityEvent _onRelease;

    private MeshRenderer buttonMesh;

    private void Start()
    {
        buttonMesh = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        //the state set materials accordingly to what they are
        switch (_state)
        {
            case ButtonState.On:
                buttonMesh.material = _buttonMaterial[0];
                break;
            case ButtonState.Pressed:
                buttonMesh.material = _buttonMaterial[1];

                break;
            case ButtonState.Off:
                buttonMesh.material = _buttonMaterial[2];

                break;
            default:
                break;
        }
    }

    //when pressed use the onClick Event
    public void OnPressed()
    {
        if (_state == ButtonState.Off)
            return;

        _onClick?.Invoke();
        _state = ButtonState.Pressed;
    }

    //when released use the onRelease Event
    public void OnRelease()
    {
        if (_state == ButtonState.Off)
            return;

        _onRelease?.Invoke();
        _state = ButtonState.On;
    }

    //Toggle button for the game manager to disable them as a random event
    public void ToggleButton(bool active)
    {
        if (active)
        {
            _state = ButtonState.On;
        }
        else
        {
            _state = ButtonState.Off;
        }
    }
}
