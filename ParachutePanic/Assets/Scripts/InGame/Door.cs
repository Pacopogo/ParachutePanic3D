using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 _moveRange;     //Range the door is allowed to move between
    [SerializeField] private float _speed = 6;       //Opening Speed;
    [SerializeField] private float _direction;       //What direction the door is moving in

    private void FixedUpdate()
    {
        //the moving of the door based on its X axis Clamped by the MoveRange
        float newXpos = transform.position.x + _direction * _speed * Time.fixedDeltaTime;
        newXpos = Mathf.Clamp(newXpos, _moveRange.x, _moveRange.y);
        transform.position = new Vector3(newXpos, transform.position.y, transform.position.z);
    }

    //Toggle Direction button event
    public void ToggleDoor()
    {
        _direction = -_direction;
    }
}
