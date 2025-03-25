using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;                        //Text element to change the score board text
    [SerializeField] private MeshRenderer screenMesh;                   //Mesh Renderer to change the material
    [SerializeField] private Material[] screenMat = new Material[2];    //Array of materials

    [SerializeField] private AudioClip alertSound;                      //Alert clip
    [SerializeField] private VoiceLines meanLines;                      //Scriptable object of all mean lines
    [SerializeField] private VoiceLines niceLines;                      //Scriptable object of all nice lines
    [SerializeField] private VoiceLines welcomeVoice;                   //Scriptable object of all welcome lines

    [SerializeField] private Life life;                                 //the life script to call in its function

    private AudioSource audioSource;
    private SceneLoader sceneLoader;

    private int currentScore;                                           //the current reached score

    private void Start()
    {
        //setup score board to make TAGS (character) face shown
        scoreText.text = ":)";

        audioSource = GetComponent<AudioSource>();
        sceneLoader = GetComponent<SceneLoader>();

        //play a random welcome voice line when started the game
        audioSource.clip = welcomeVoice.line[Random.Range(0, welcomeVoice.line.Length)];
        audioSource.Play();
    }

    //add score function that calls in the logic for voice lines and text change
    public void startAddScore()
    {
        currentScore += 10;
        StartCoroutine(addScore());
    }

    //When missed function to call the Missed logic for voice line and text
    public void startMissedTrash()
    {
        StartCoroutine(missedTrash());
    }

    private IEnumerator missedTrash()
    {
        if (life.currentLife > 0)
        {
            //set the background to red and  play a alert clip
            screenMesh.material = screenMat[1];
            scoreText.text = ">:(";
            audioSource.clip = alertSound;
            audioSource.Play();

            //wait until clip is over
            yield return new WaitForSeconds(audioSource.clip.length);

            //Play voice line for the times you missed
            audioSource.clip = meanLines.line[Random.Range(0, meanLines.line.Length)];
            audioSource.Play();
            scoreText.text = "";

            //wait until the end of voice line to change it back
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
        //change mat and text while playing a nice voice line
        screenMesh.material = screenMat[0];
        scoreText.text = ":)";
        audioSource.clip = niceLines.line[Random.Range(0, niceLines.line.Length)];
        audioSource.Play();

        //wait until end of voice line
        yield return new WaitForSeconds(audioSource.clip.length);

        scoreText.text = currentScore.ToString();

        //if you reached the goal load winning scene
        if (currentScore >= 200)
            sceneLoader.LoadSceneIndex(3);

        StopAllCoroutines();
    }
}
