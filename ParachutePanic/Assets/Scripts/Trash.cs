using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private Life life;
    [SerializeField] private Scoreboard scoreboard;

    private void Start()
    {
        life = Object.FindObjectOfType<Life>();
        scoreboard = Object.FindObjectOfType<Scoreboard>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("kart"))
        {
            scoreboard.startAddScore();
            Object.Destroy(gameObject);

        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            life.LoseLife();
            scoreboard.startMissedTrash();
            Object.Destroy(gameObject);
        }

    }
}
