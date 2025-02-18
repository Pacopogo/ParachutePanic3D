using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private Life life;                      //Life logic script
    private Scoreboard scoreboard;          //Scoreboard logic script
    private GameEventManager gameMaster;    //GameMaster logic script

    private void Start()
    {
        //Setup for the trash
        if (gameMaster == null)
        {
            gameMaster = FindObjectOfType<GameEventManager>();
        }

        life        = GameObject.FindObjectOfType<Life>();
        scoreboard  = GameObject.FindObjectOfType<Scoreboard>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //when hit the kart add score and when hit the ground Lose a life
        if (collision.gameObject.CompareTag("kart"))
        {
            scoreboard.startAddScore();
            collision.gameObject.GetComponent<AudioSource>().Play();
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
