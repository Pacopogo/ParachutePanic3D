using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] private GameEventManager gameMaster;       //Game master component
    [SerializeField] private GameObject trashObj;               //Trash Prefab to be dropped
    [SerializeField] private Transform dropTrans;               //The Pivot point to drop the trash from
    [SerializeField] private ParticleSystem particle;           //The Unique particles for dropping the trash

    [Header("Settings")]
    [SerializeField] private Vector2 moveRange;                 //The range the Droppers are allowed to move between
    [SerializeField] private float speed = 2;                   //The movement Speed
    private bool dir = true;                                    //What direction they move

    private AudioSource audioSource;                            //Audio component

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (gameMaster == null)
        {
            gameMaster = FindObjectOfType<GameEventManager>();
        }
    }

    private void FixedUpdate()
    {
        MoveDropper();
    }

    //The dropper movement that shifts everytime it has reached its max/min range
    private void MoveDropper()
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

    //Droping trash function with a extra safe guard to prevent the max amount of trash to drop
    public void DropTrash()
    {
        if (gameMaster.trashObj.Count < gameMaster.MaxTrashAmount)
        {
            audioSource.Play();
            particle.Play();

            //Make the trash and add it to the trash list to limit the amount that can be dropped
            GameObject trash = Instantiate(trashObj, dropTrans.position, dropTrans.rotation);
            gameMaster.trashObj.Add(trash);
        }
        else
        {
            return;
        }
    }
}
