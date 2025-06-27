using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public PacoButton[] Buttons;
    public static ButtonManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void DisableRandomButton()
    {
        int rnd = Random.Range(0, Buttons.Length);

        Buttons[rnd].ToggleButton(false);
    }

    public void EnableAllButtons()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].ToggleButton(false);
        }
    }

}
