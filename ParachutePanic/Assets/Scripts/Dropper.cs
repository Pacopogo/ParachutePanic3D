using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] private GameObject trashObj;
    [SerializeField] private Transform dropTrans;

    [Header("Settings")]
    [SerializeField] private Vector2 moveRange;
    [SerializeField] private float speed = 2;
    private bool dir = true;

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

    public IEnumerator DropTrash(float timer)
    {
        yield return new WaitForSeconds(timer);
        Instantiate(trashObj, dropTrans.position, dropTrans.rotation);
    }
}
