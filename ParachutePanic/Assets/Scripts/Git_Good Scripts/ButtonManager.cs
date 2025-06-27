using System.Collections;
using UnityEngine;

public class ButtonManager : MonoBehaviour, IManager
{

    [Header("Settings")]
    [SerializeField] private float _startTime = 5;
    [SerializeField] private Vector2 _timerRange = new Vector2(3, 8);

    [SerializeField] private SimpleAudioPlayer _audioPlayer;
    [SerializeField] private AudioClip _clip;

    public PacoButton[] Buttons;
    public static ButtonManager instance;

    private void Awake() => instance = this;
    private void Start() => StartCoroutine(BreakButton(_startTime));

    public void DisableRandomButton()
    {
        int rnd = Random.Range(0, Buttons.Length);

        Buttons[rnd].ToggleButton(false);
        _audioPlayer.PlayClip(_clip);
    }

    public void EnableAllButtons()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].ToggleButton(false);
        }
    }

    private IEnumerator BreakButton(float waitTime)
    {

        int amount = Random.Range(1, Buttons.Length - 2);
        float time = Random.Range(_timerRange.x, _timerRange.y);

        yield return new WaitForSeconds(waitTime);

        for (int i = 0;i < amount; i++)
        {
            yield return new WaitForSeconds(0.2f);
            DisableRandomButton();
        }

        StartCoroutine(BreakButton(time));
        StopCoroutine(BreakButton(waitTime));
    }

}
