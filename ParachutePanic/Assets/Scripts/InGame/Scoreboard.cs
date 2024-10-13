using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private MeshRenderer screenMesh;
    [SerializeField] private Material[] screenMat = new Material[2];

    private AudioSource audioSource;

    private int currentScore;

    private void Start()
    {
        scoreText.text = ":)";
        audioSource = GetComponent<AudioSource>();
    }

    public void startAddScore()
    {
        currentScore += 10;
        StartCoroutine(addScore());
    }

    public void startMissedTrash()
    {
        StartCoroutine(missedTrash());
    }

    private IEnumerator missedTrash()
    {
        screenMesh.material = screenMat[1];
        scoreText.text = ">:(";
        audioSource.Play();
        yield return new WaitForSeconds(1);

        screenMesh.material = screenMat[0];
        scoreText.text = currentScore.ToString();

        StopAllCoroutines();
    }
    private IEnumerator addScore()
    {

        scoreText.text = ":)";

        yield return new WaitForSeconds(0.5f);

        scoreText.text = currentScore.ToString();

        StopAllCoroutines();
    }
}
