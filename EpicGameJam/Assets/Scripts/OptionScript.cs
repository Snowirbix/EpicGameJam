using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    public Options options;

    public Button azerty;

    public Button qwerty;

    public void DisplayChoosen()
    {
        if(options.keyboard == Options.Keyboard.QWERTY)
        {
            qwerty.Select();
        }
        else
        {
            azerty.Select();
        }
    }
    public void Azerty()
    {
        options.keyboard = Options.Keyboard.AZERTY;
    }

    public void Qwerty()
    {
        options.keyboard = Options.Keyboard.QWERTY;
    }
}
