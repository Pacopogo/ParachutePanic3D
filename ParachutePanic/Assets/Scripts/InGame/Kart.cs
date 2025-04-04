using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 _moveRange = new Vector2(-1, 15);
    [SerializeField] private float _speed = 6;
    private float _direction;
    private bool _isMoving = false;

    [SerializeField] private ParticleSystem _brokenParticles;
    public bool _isOn = true;

    private void FixedUpdate()
    {
        //check if it is broken or not and allowed to move
        if (!_isMoving || !_isOn)
            return;

        //move with given direction
        float newXpos = transform.position.x + _direction * _speed * Time.fixedDeltaTime;
        newXpos = Mathf.Clamp(newXpos, _moveRange.x, _moveRange.y);
        transform.position = new Vector3(newXpos, transform.position.y, transform.position.z);

    }
    public void MoveKart(float dir)
    {
        _direction = dir;
        return;
    }
    public void ToggleKart(bool move)
    {
        _isMoving = move;
        return;
    }

    //the function to break the kart with appropriate particles
    public void ToggleActiveKart(bool active)
    {
        _isOn = active;
        if (_isOn)
        {
            _brokenParticles.Stop();
        }
        else
        {
            _brokenParticles.Play();
        }
        return;
    }

}
