using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trash : MonoBehaviour
{
    private Life life;                      //Life logic script
    private Scoreboard scoreboard;          //Scoreboard logic script
    private GameEventManager gameManager;   //GameMaster logic script

    [SerializeField] private UnityEvent OnHitKart;
    [SerializeField] private UnityEvent OnHitGround;

    private void Start()
    {
        //Setup for the trash
        gameManager = FindObjectOfType<GameEventManager>();
        life = FindObjectOfType<Life>();
        scoreboard = FindObjectOfType<Scoreboard>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        //when hit the kart add score and when hit the ground Lose a life
        if (collision.gameObject.layer == 7)
        {
            OnHitKart?.Invoke();

            HitKart();
        }
        else if (collision.gameObject.layer == 8)
        {
            OnHitGround?.Invoke();

            HitGround();
        }

    }

    private void HitKart()
    {
        scoreboard.startAddScore();
        gameManager.SetAmountDropped(-1);

        gameObject.SetActive(false);
    }

    private void HitGround()
    {
        life.LoseLife();
        gameManager.SetAmountDropped(-1);

        scoreboard.startMissedTrash();

        gameObject.SetActive(false);
    }
}
