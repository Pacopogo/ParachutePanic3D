using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class playerMovement : MonoBehaviour
{
    [Header("Settings")]                            //these are the settings for the player
    [SerializeField] private float wSpeed = 6;      //walking speed
    [SerializeField] private float rSpeed = 6;      //rotation speed

    private Vector2 newPlayerVec;                   //the new given horizontal vector for the player
    private Vector2 rAxis;                          //the rotation axis for the player

    [Header("Interactable")]
    [SerializeField] private GameObject camObj;
    [SerializeField] private LayerMask interactLayer;
    private RaycastHit hit;
    [SerializeField] private float maxDistance;
    private PacoButton pacoButton;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
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
        float lookX = rAxis.x * rSpeed * Time.fixedDeltaTime;
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
                    pacoButton = null;
                }
                else 
                {
                    pacoButton.OnPressed();
                }
            }
        }
        if (input.canceled && pacoButton != null)
        {
            pacoButton.OnRelease();
            pacoButton = null;
        }
    }
    #endregion

}
