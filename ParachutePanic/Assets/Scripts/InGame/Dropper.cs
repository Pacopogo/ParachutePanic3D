using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] private GameEventManager gameMaster;
    [SerializeField] private GameObject trashObj;
    [SerializeField] private Transform dropTrans;
    [SerializeField] private ParticleSystem particle;

    [Header("Settings")]
    [SerializeField] private Vector2 moveRange;
    [SerializeField] private float speed = 2;
    private bool dir = true;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (gameMaster == null)
        {
            gameMaster = FindObjectOfType<GameEventManager>();
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (gameMaster == null)
        {
            gameMaster = FindObjectOfType<GameEventManager>();
        }
    }

    private void FixedUpdate()
    {
        if (dir)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * -speed * Time.deltaTime);
        }

        if (transform.position.x >= moveRange.y || transform.position.x <= moveRange.x)
        {
            dir = !dir;
        }
    }

    public void DropTrash()
    {
        if (gameMaster.trashObj.Count < gameMaster.MaxTrashAmount)
        {
            audioSource.Play();
            particle.Play();
            GameObject trash = Instantiate(trashObj, dropTrans.position, dropTrans.rotation);
            gameMaster.trashObj.Add(trash);
        }
        else
        {
            return;
        }
    }
}
