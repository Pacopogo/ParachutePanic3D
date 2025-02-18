using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameEventManager gameMaster;
    [SerializeField] private GameObject[] lifeLights;
    [SerializeField] private Material[] materials;
    private MeshRenderer currentMesh;
    private AudioSource audioSoruce;
    private SceneLoader sceneLoader;

    public int currentLife = 3;

    private void Start()
    {
        audioSoruce = GetComponent<AudioSource>();
        sceneLoader = GetComponent<SceneLoader>();

        if (gameMaster == null)
        {
            gameMaster = FindObjectOfType<GameEventManager>();
        }
    }

    //function to remove the lifes between the max amount and 0
    public void LoseLife()
    {
        currentLife--;
        currentLife = Mathf.Clamp(currentLife, 0, lifeLights.Length);

        for (int i = 0; i < lifeLights.Length; i++)
        {
            //when lost a life change material to read
            currentMesh = lifeLights[i].GetComponent<MeshRenderer>();
            if (currentLife <= i)
            {
                currentMesh.material = materials[1];
            }
            else
            {
                currentMesh.material = materials[0];

            }
        }

        //when life hit zero you die
        if (currentLife <= 0)
            StartCoroutine(YouDied());

    }

    //function to call all final functions to show you are dead
    private IEnumerator YouDied()
    {
        yield return new WaitForSeconds(1);
        gameMaster.endGame();
        audioSoruce.Play();
        gameOverUI.SetActive(true);
        yield return new WaitForSeconds(audioSoruce.clip.length);
        sceneLoader.LoadSceneIndex(0);
    }
}
