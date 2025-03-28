using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 moveRange;     //Range the door is allowed to move between
    [SerializeField] private float speed = 6;       //Opening Speed;
    [SerializeField] private float direction;       //What direction the door is moving in

    private void FixedUpdate()
    {
        //the moving of the door based on its X axis Clamped by the MoveRange
        float newXpos = transform.position.x + direction * speed * Time.fixedDeltaTime;
        newXpos = Mathf.Clamp(newXpos, moveRange.x, moveRange.y);
        transform.position = new Vector3(newXpos, transform.position.y, transform.position.z);
    }

    //Toggle Direction button event
    public void ToggleDoor()
    {
        direction = -direction;
    }
}
