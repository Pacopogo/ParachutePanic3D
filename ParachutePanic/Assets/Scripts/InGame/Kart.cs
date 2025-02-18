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

    [SerializeField] private ParticleSystem particle;
    private bool isOn = true;

    private void FixedUpdate()
    {
        //check if it is broken or not and allowed to move
        if (!isMoving || !isOn)
            return;

        //move with given direction
        float newXpos = transform.position.x + direction * speed * Time.fixedDeltaTime;
        newXpos = Mathf.Clamp(newXpos, MoveRange.x, MoveRange.y);
        transform.position = new Vector3(newXpos, transform.position.y, transform.position.z);

    }
    public void MoveKart(float dir)
    {
        Debug.Log(dir + " is the current dir");
        direction = dir;
        return;
    }
    public void toggleKart(bool move)
    {
        isMoving = move;
        return;
    }

    //the function to break the kart with appropriate particles
    public void toggleActiveKart(bool active)
    {
        isOn = active;
        if (isOn)
        {
            particle.Stop();
        }
        else
        {
            particle.Play();
        }
        return;
    }

}
