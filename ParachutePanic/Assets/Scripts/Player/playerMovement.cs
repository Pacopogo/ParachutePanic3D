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
    [SerializeField] private playerSettings playerSettings;

    [Header("Settings")]                                                //these are the settings for the player
    [Tooltip("Player Movement speed")]
    [SerializeField] private float wSpeed = 6;                          //walking speed
    private float rSpeed;                                               //rotation speed

    [Header("Camera component")]
    [SerializeField] private CinemachineVirtualCamera cinneVirtual;     //the vertiual camera
    private CinemachinePOV Pov;                                         //The vertiual camera compontent stored

    private Vector2 newPlayerVec;                                       //the new given horizontal vector for the player
    private Vector2 rAxis;                                              //the rotation axis for the player

    [Header("Interactable")]
    [SerializeField] private GameObject camObj;                         //the camera Object
    [SerializeField] private GameObject tooltipObj;                     //the visualization for the interaction button
    [SerializeField] private LayerMask interactLayer;                   //Interaction layer
    private RaycastHit hit;
    [SerializeField] private float maxDistance;                         //The distance the player has to be from a interactable to interact with it
    private PacoButton pacoButton;

    void Start()
    {
        rSpeed = playerSettings.mouseSensitivity;

        cinemachineSetup();
    }

    private void cinemachineSetup()
    {
        //This sets up the Vertical camera clamp using Cinnemachine
        //you have to manually add the componant to the cinnemachine to adjust its settings
        Pov = cinneVirtual.AddCinemachineComponent<CinemachinePOV>();           
        Pov.m_HorizontalAxis.m_MaxSpeed = 0;
        Pov.m_VerticalAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
        
        //Lock the Cursor the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Pov.m_VerticalAxis.m_MaxSpeed = rSpeed;

        playerMove();           //The Movement logic 
        playerRot();            //The Player Horizontal Rotation logic 
        tooltipCheck();         //The tooltip logic

    }

    private void playerMove()
    {
        //The input multiplied by Speed and the fixed delta time to give the new direction
        float playerForward = newPlayerVec.y * wSpeed * Time.fixedDeltaTime;
        float playerRight = newPlayerVec.x * wSpeed * Time.fixedDeltaTime;

        //Forwad and Backwards movement
        transform.Translate(Vector3.forward * playerForward);

        //Left and Right movement
        transform.Translate(Vector3.right * playerRight);
    }

    private void playerRot()
    {
        float lookX = rAxis.x * (rSpeed * 1.2f) * Time.fixedDeltaTime;
        transform.Rotate(0, lookX, 0);
    }

    //input value readings
    #region inputEvents
    public void horizontalMove(InputAction.CallbackContext input) {

        newPlayerVec = input.ReadValue<Vector2>();
    }

    public void playerlook(InputAction.CallbackContext input)
    {
        rAxis = input.ReadValue<Vector2>();
    }

    public void playerButton(InputAction.CallbackContext input) {
        //check if it reaches a interactable
        if (Physics.Raycast(camObj.transform.position, camObj.transform.forward, out hit, maxDistance, interactLayer))
        {
            //checks if it has a Pacobutton
            if (hit.collider.GetComponent<PacoButton>())
            {
                //play correct event on correct input release or press
                if(input.performed)
                {
                    pacoButton = hit.collider.GetComponent<PacoButton>();   //Store Button into pacoButton
                    pacoButton.OnPressed();
                }
                else if(input.canceled && pacoButton != null)
                {
                    pacoButton.OnRelease();
                    pacoButton = null;                                      //Remove stored Button
                }
            }
        }
        else if (input.canceled && pacoButton != null)
        {
            pacoButton.OnRelease();
            pacoButton = null;
        }
    }
    #endregion

    private void tooltipCheck()
    {
        //if you hover over a interactable it will show you the interact UI
        if (Physics.Raycast(camObj.transform.position, camObj.transform.forward, out hit, maxDistance, interactLayer))
        {
            tooltipObj.SetActive(true);
        }
        else
        {
            tooltipObj.SetActive(false);
        }
    }
}
