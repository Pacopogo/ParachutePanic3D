using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private bool _godMode = false;

    [Header("Components")]
    [SerializeField] private GameEventManager _gameMaster;

    [Header("Gameobjects")]
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject[] _lifeLights;

    [Header("Materials")]
    [SerializeField] private Material[] _materials;

    private MeshRenderer _currentMesh;
    private AudioSource _audioSoruce;
    private SceneLoader _sceneLoader;

    public int CurrentLife = 3;

    private void Start()
    {
        _audioSoruce = GetComponent<AudioSource>();
        _sceneLoader = GetComponent<SceneLoader>();

        if (_gameMaster == null)
        {
            _gameMaster = FindObjectOfType<GameEventManager>();
        }
    }

    //function to remove the lifes between the max amount and 0
    public void LoseLife()
    {
        if (_godMode)
            return;

        CurrentLife--;
        CurrentLife = Mathf.Clamp(CurrentLife, 0, _lifeLights.Length);

        for (int i = 0; i < _lifeLights.Length; i++)
        {
            //when lost a life change material to read
            _currentMesh = _lifeLights[i].GetComponent<MeshRenderer>();
            if (CurrentLife <= i)
            {
                _currentMesh.material = _materials[1];
            }
            else
            {
                _currentMesh.material = _materials[0];

            }
        }

        //when life hit zero you die
        if (CurrentLife <= 0)
            StartCoroutine(YouDied());

    }

    //function to call all final functions to show you are dead
    private IEnumerator YouDied()
    {

        yield return new WaitForSeconds(1);

        _gameMaster.EndGame();
        _audioSoruce.Play();
        _gameOverUI.SetActive(true);

        yield return new WaitForSeconds(_audioSoruce.clip.length);

        _sceneLoader.LoadSceneIndex(0);
    
    }
}
