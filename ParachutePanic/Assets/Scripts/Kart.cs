using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 MoveRange;
    [SerializeField] private float speed = 6;
    private float direction;
    private bool isMoving = false;


    private void FixedUpdate()
    {
        if (!isMoving)
            return;

        float newXpos = transform.position.x + direction * speed * Time.fixedDeltaTime;
        newXpos = Mathf.Clamp(newXpos, MoveRange.x, MoveRange.y);
        transform.position = new Vector3(newXpos, transform.position.y, transform.position.z);
    }
    public void MoveKart(float dir)
    {
        direction = dir;
        return;
    }
    public void toggleKart(bool move) 
    {
        isMoving = move;
        return;
    }

}
