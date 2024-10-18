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

    [Header("Settings")]                                            //these are the settings for the player
    [Tooltip("Player Movement speed")]
    [SerializeField] private float wSpeed = 6;                      //walking speed
    private float rSpeed;                                           //rotation speed

    [Header("Camera component")]
    [SerializeField] private CinemachineVirtualCamera cinneVirtual;
    [SerializeField] private GameObject camObj;
    private CinemachinePOV Pov;

    private Vector2 newPlayerVec;                                   //the new given horizontal vector for the player
    private Vector2 rAxis;                                          //the rotation axis for the player

    [Header("Interactable")]
    [SerializeField] private LayerMask interactLayer;
    private RaycastHit hit;
    [SerializeField] private float maxDistance;
    private PacoButton pacoButton;
    void Start()
    {
        rSpeed = playerSettings.mouseSensitivity;
        cinemachineSetup();
    }

    private void cinemachineSetup()
    {
        Pov = cinneVirtual.AddCinemachineComponent<CinemachinePOV>();
        Pov.m_HorizontalAxis.m_MaxSpeed = 0;
        Pov.m_VerticalAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Pov.m_VerticalAxis.m_MaxSpeed = rSpeed;

        playerMove();
        playerCamera();

    }

    private void playerMove()
    {
        float playerForward = newPlayerVec.y * wSpeed * Time.fixedDeltaTime;
        float playerRight = newPlayerVec.x * wSpeed * Time.fixedDeltaTime;

        transform.Translate(Vector3.forward * playerForward);
        transform.Translate(Vector3.right * playerRight);
    }

    private void playerCamera()
    {
        float lookX = rAxis.x * (rSpeed * 1.2f) * Time.fixedDeltaTime;
        transform.Rotate(0, lookX, 0);
    }

    #region inputEvents
    public void horizontalMove(InputAction.CallbackContext input) {

        newPlayerVec = input.ReadValue<Vector2>();
    }

    public void playerlook(InputAction.CallbackContext input)
    {
        rAxis = input.ReadValue<Vector2>();
    }

    public void playerButton(InputAction.CallbackContext input) {

        if (Physics.Raycast(camObj.transform.position, camObj.transform.forward, out hit, maxDistance, interactLayer))
        {
            if (hit.collider.GetComponent<PacoButton>())
            {
                pacoButton = hit.collider.GetComponent<PacoButton>();
                if (input.canceled)
                {
                    pacoButton.OnRelease();
                }
                else if(input.performed)
                {
                    pacoButton.OnPressed();
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

}
