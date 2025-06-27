using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartManager : MonoBehaviour, IManager
{
    [SerializeField] private Kart[] _karts;

    [Header("Settings")]
    [SerializeField] private float _startTimer = 15;
    [SerializeField] private Vector2 _timer = new Vector2(8,15);

    [Header("Audio Settings")]
    [SerializeField] private SimpleAudioPlayer _audioPlayer;
    [SerializeField] private AudioClip _breakClip;

    public static KartManager instance;
    private void Awake() => instance = this;


    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void Start()
    {
        StartCoroutine(BreakKart(_startTimer));
    }

    public void BreakRandomKart()
    {
        int rnd = Random.Range(0, _karts.Length);
        _karts[rnd].ToggleActiveKart(false);

        _audioPlayer.PlayClip(_breakClip);
    }

    public void BreakKartIndex(int index)
    {
        _karts[index].ToggleActiveKart(false);

        _audioPlayer.PlayClip(_breakClip);
    }

    public void RepairAllKarts()
    {
        for (int i = 0; i < _karts.Length; i++)
        {
            _karts[i].ToggleActiveKart(true);
        }
    }

    private IEnumerator BreakKart(float waitTime)
    {
        Debug.Log("Dropping");
        int amount = Random.Range(1, _karts.Length);
        float time = Random.Range(_timer.x, _timer.y);

        yield return new WaitForSeconds(waitTime);

        BreakRandomKart();
       

        StartCoroutine(BreakKart(time));

        StopCoroutine(BreakKart(waitTime));
    }

}
