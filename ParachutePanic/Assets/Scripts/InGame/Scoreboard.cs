using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private MeshRenderer _screenMesh;
    [SerializeField] private Life _life;

    [Header("Materials")]
    [SerializeField] private Material[] _screenMat = new Material[2];

    [Header("Audio clips and voice lines")]
    [SerializeField] private AudioClip _alertSound;
    [SerializeField] private VoiceLines _meanLines;
    [SerializeField] private VoiceLines _niceLines;
    [SerializeField] private VoiceLines _welcomeVoice;

    //Note: Feedback from Bas to work with a const string
    [Header("Faces")]
    private const string HAPPY_FACE = ":)";
    private const string ANGRY_FACE = ">:(";


    private AudioSource _audioSource;
    private SceneLoader _sceneLoader;

    private int currentScore;                                           //the current reached score

    private void Start()
    {
        //setup score board to make TAGS (character) face shown
        _scoreText.text = HAPPY_FACE;

        _audioSource = GetComponent<AudioSource>();
        _sceneLoader = GetComponent<SceneLoader>();

        //play a random welcome voice line when started the game
        _audioSource.clip = _welcomeVoice.line[Random.Range(0, _welcomeVoice.line.Length)];
        _audioSource.Play();
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
        if (_life.currentLife > 0)
        {
            //set the background to red and  play a alert clip
            _screenMesh.material = _screenMat[1];
            _scoreText.text = ANGRY_FACE;

            _audioSource.clip = _alertSound;
            _audioSource.Play();

            //wait until clip is over
            yield return new WaitForSeconds(_audioSource.clip.length);

            //Play voice line for the times you missed
            _audioSource.clip = _meanLines.line[Random.Range(0, _meanLines.line.Length)];
            _audioSource.Play();

            _scoreText.text = "";

            //wait until the end of voice line to change it back
            yield return new WaitForSeconds(_audioSource.clip.length);
            _screenMesh.material = _screenMat[0];
            _scoreText.text = currentScore.ToString();
        }

        _screenMesh.material = _screenMat[0];
        _scoreText.text = currentScore.ToString();

        StopAllCoroutines();
    }
    private IEnumerator addScore()
    {
        //change mat and text while playing a nice voice line
        _screenMesh.material = _screenMat[0];
        _scoreText.text = HAPPY_FACE;

        _audioSource.clip = _niceLines.line[Random.Range(0, _niceLines.line.Length)];
        _audioSource.Play();

        //wait until end of voice line
        yield return new WaitForSeconds(_audioSource.clip.length);

        _scoreText.text = currentScore.ToString();

        //if you reached the goal load winning scene
        if (currentScore >= 200)
            _sceneLoader.LoadSceneIndex(3);

        StopAllCoroutines();
    }
}
