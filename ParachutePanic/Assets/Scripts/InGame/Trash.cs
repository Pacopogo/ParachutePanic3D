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
        this.gameMaster     = FindObjectOfType<GameEventManager>();
        this.life           = FindObjectOfType<Life>();
        this.scoreboard     = FindObjectOfType<Scoreboard>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //when hit the kart add score and when hit the ground Lose a life
        if (collision.gameObject.CompareTag("kart"))
        {
            this.scoreboard.startAddScore();
            this.gameMaster.SetAmountDropped(-1);
           
            collision.gameObject.GetComponent<AudioSource>().Play();

            this.gameObject.SetActive(false);
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            this.life.LoseLife();
            this.gameMaster.SetAmountDropped(-1);

            this.scoreboard.startMissedTrash();
            
            this.gameObject.SetActive(false);
        }

    }
}
