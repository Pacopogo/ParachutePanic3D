using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 MoveRange;
    [SerializeField] private float speed = 6;
    [SerializeField] private float direction;
    private bool isMoving = true;

    private void FixedUpdate()
    {
        float newXpos = transform.position.x + direction * speed * Time.fixedDeltaTime;
        newXpos = Mathf.Clamp(newXpos, MoveRange.x, MoveRange.y);
        transform.position = new Vector3(newXpos, transform.position.y, transform.position.z);
    }

    public void toggleDoor()
    {
        direction = -direction;
    }
}
