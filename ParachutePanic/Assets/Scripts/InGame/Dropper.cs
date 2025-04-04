using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] private GameEventManager _gameMaster;
    [SerializeField] private Transform _dropTrans;
    [SerializeField] private ParticleSystem _particle;

    [Header("Settings")]
    [SerializeField] private Vector2 _moveRange;
    [SerializeField] private float _speed = 2;
    private bool _dir = true;

    [SerializeField] private AudioSource _audioSource;

    private void FixedUpdate()
    {
        MoveDropper();
    }

    //The dropper movement that shifts everytime it has reached its max/min range
    private void MoveDropper()
    {
        if (_dir)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * -_speed * Time.deltaTime);
        }

        if (transform.position.x >= _moveRange.y || transform.position.x <= _moveRange.x)
        {
            _dir = !_dir;
        }
    }

    public void DropTrash()
    {
        int rnd = Random.Range(0, 3);

        _audioSource.Play();
        _particle.Play();

        //Make the trash and add it to the trash list to limit the amount that can be dropped
        GameObject trash = Objectpool.Instance.GetSpesificPool(rnd);

        if (trash == null)
        {
            Objectpool.Instance.AddPool(1);
            trash = Objectpool.Instance.GetSpesificPool(rnd);
        }

        trash.transform.position = _dropTrans.transform.position;
        trash.transform.rotation = _dropTrans.transform.rotation;

        trash.SetActive(true);

    }
}
