using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class playerMovement : MonoBehaviour
{
    [SerializeField] private playerSettings _playerSettings;

    [Header("Settings")]
    [Tooltip("Player Movement speed")]
    [SerializeField] private float _speed = 10;
    
    private float _rotationSpeed;

    [Header("Camera component")]
    [SerializeField] private GameObject _camObj;
    [SerializeField] private CinemachineVirtualCamera _cinneVirtual;
    
    private CinemachinePOV _pov;

    private Vector2 _newPlayerVec;
    private Vector2 _rotationAxis;

    [Header("Interactable")]
    [SerializeField] private GameObject _tooltipObj;
    [SerializeField] private LayerMask _interactLayer;
    [SerializeField] private float _maxDistance = 6;

    private PacoButton _pacoButton;
    private RaycastHit _hit;

    void Start()
    {
        _rotationSpeed = _playerSettings.MouseSensitivity;

        CinemachineSetup();
    }

    private void CinemachineSetup()
    {
        //This sets up the Vertical camera clamp using Cinnemachine
        //you have to manually add the componant to the cinnemachine to adjust its settings
        _pov = _cinneVirtual.AddCinemachineComponent<CinemachinePOV>();           
        _pov.m_HorizontalAxis.m_MaxSpeed = 0;
        _pov.m_VerticalAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
        
        //Lock the Cursor the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        _pov.m_VerticalAxis.m_MaxSpeed = _rotationSpeed;

        PlayerMove();           //The Movement logic 
        PlayerRot();            //The Player Horizontal Rotation logic 
        ToolTipCheck();         //The tooltip logic

    }

    private void PlayerMove()
    {
        //The input multiplied by Speed and the fixed delta time to give the new direction
        float playerForward = _newPlayerVec.y * _speed * Time.fixedDeltaTime;
        float playerRight = _newPlayerVec.x * _speed * Time.fixedDeltaTime;

        //Forwad and Backwards movement
        transform.Translate(Vector3.forward * playerForward);

        //Left and Right movement
        transform.Translate(Vector3.right * playerRight);
    }

    //Player rotation and camera rotation
    private void PlayerRot()
    {
        float lookX = _rotationAxis.x * (_rotationSpeed * 1.2f) * Time.fixedDeltaTime;
        transform.Rotate(0, lookX, 0);
    }

    //input value readings
    #region inputEvents
    public void HorizontalMove(InputAction.CallbackContext input) 
    {
        _newPlayerVec = input.ReadValue<Vector2>();
    }

    public void Playerlook(InputAction.CallbackContext input)
    {
        _rotationAxis = input.ReadValue<Vector2>();
    }

    public void PlayerButton(InputAction.CallbackContext input) 
    {
        //check if it reaches a interactable
        if (Physics.Raycast(_camObj.transform.position, _camObj.transform.forward, out _hit, _maxDistance, _interactLayer))
        {
            //checks if it has a Pacobutton
            if (_hit.collider.GetComponent<PacoButton>())
            {
                //play correct event on correct input release or press
                if(input.performed)
                {
                    _pacoButton = _hit.collider.GetComponent<PacoButton>();   //Store Button into pacoButton
                    _pacoButton.OnPressed();
                }
                else if(input.canceled && _pacoButton != null)
                {
                    _pacoButton.OnRelease();
                    _pacoButton = null;                                      //Remove stored Button
                }
            }
        }
        else if (input.canceled && _pacoButton != null)
        {
            _pacoButton.OnRelease();
            _pacoButton = null;
        }
    }
    #endregion

    private void ToolTipCheck()
    {
        //if you hover over a interactable it will show you the interact UI
        if (Physics.Raycast(_camObj.transform.position, _camObj.transform.forward, out _hit, _maxDistance, _interactLayer))
        {
            _tooltipObj.SetActive(true);
        }
        else
        {
            _tooltipObj.SetActive(false);
        }
    }
}
