using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class HenryEvent : MonoBehaviour
{
    [SerializeField] private InputActionReference action;
    // -1 means infinit
    [SerializeField] int uses = 1;

    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private UnityEvent OnLook;
    [SerializeField] private UnityEvent OnInteract;

    private Transform _selection;
    private bool Look;

    public bool isInteracting = false;

    private void OnEnable()
    {
        action.action.performed += interact;
        action.action.Enable();
    }

    private void OnDisable()
    {
        action.action.performed -= interact;
        action.action.Disable();
    }
    private void FixedUpdate()
    {
        HenryLook();
    }

    private void HenryLook()
    {


        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();

            _selection = null;
        }

        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag) && hit.transform == transform)
            {
                if (Look == true && uses > 0)
                {
                    uses--;
                    OnLook?.Invoke();
                    _selection = selection;
                    Look = false;
                }
                if (isInteracting)
                {
                    OnInteract?.Invoke();
                }
            }
            else
            {
                Look = true;
            }
        }
    }

    public void interact(InputAction.CallbackContext context)
    {

        isInteracting = context.performed;

    }

}