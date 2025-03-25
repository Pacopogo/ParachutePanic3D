using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private PacoButton[] Buttons;
    public static ButtonManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void DisableRandomButton()
    {
        int rnd = Random.Range(0, Buttons.Length);

        Buttons[rnd].toggleButton(false);
    }

    public void EnableAllButtons()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].toggleButton(false);
        }
    }

}
