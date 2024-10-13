using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private MeshRenderer screenMesh;
    [SerializeField] private Material[] screenMat = new Material[2];

    [SerializeField] private AudioClip alertSound;
    [SerializeField] private VoiceLines meanLines;
    [SerializeField] private VoiceLines niceLines;
    [SerializeField] private VoiceLines welcomeVoice;

    [SerializeField] private Life life;

    private AudioSource audioSource;

    private int currentScore;

    private void Start()
    {
        scoreText.text = ":)";
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = welcomeVoice.line[Random.Range(0, welcomeVoice.line.Length)];
        audioSource.Play();
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
        audioSource.clip = alertSound;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        if(life.currentLife > 0)
        {
            audioSource.clip = meanLines.line[Random.Range(0, meanLines.line.Length)];
            audioSource.Play();
            scoreText.text = "";
            yield return new WaitForSeconds(audioSource.clip.length);
            screenMesh.material = screenMat[0];
            scoreText.text = currentScore.ToString();
        }

        screenMesh.material = screenMat[0];
        scoreText.text = currentScore.ToString();

        StopAllCoroutines();
    }
    private IEnumerator addScore()
    {
        screenMesh.material = screenMat[0];
        scoreText.text = ":)";
        audioSource.clip = niceLines.line[Random.Range(0, niceLines.line.Length)];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        scoreText.text = currentScore.ToString();

        StopAllCoroutines();
    }
}
