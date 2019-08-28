using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Options options;
    public MessageObject[] StepQ;

    public MessageObject[] StepA;

    private MessageObject[] Step;

    bool firstStep = false;
    bool secondStep = false;
    
    public Image fade;

    private Vector2 axes;

    bool down,left,right = false;
    void Start()
    {
        if(options.keyboard == Options.Keyboard.QWERTY)
        {
            Step = StepQ;
        }
        else
        {
            Step = StepA;
        }

        MessageBox.instance.Display(new MessageBox.Message(Step[0].messageTitle, Step[0].messagecontent, Step[0].timer));
        firstStep = true;
    }

    void FixedUpdate()
    {
        if(fade.color.a != 0f)
        {
            fade.color = new Color(fade.color.r, fade.color.g , fade.color.b , fade.color.a - 0.005f);
        }
        else
        {
            fade.gameObject.SetActive(false);
        }

        if(firstStep)
        {
            if(PlayerController.instance.axes.y == 1)
            {
                firstStep = false;
                MessageBox.instance.Display(new MessageBox.Message(Step[1].messageTitle, Step[1].messagecontent, Step[1].timer));
                secondStep = true;
            }
        }

        if(secondStep)
        {
            switch(PlayerController.instance.axes.x)
            {
                case 1:
                right = true;
                break;
                case -1:
                left = true;
                break;
            }
            if(PlayerController.instance.axes.y == -1){down = true;}

            if(down && right && left)
            {
                secondStep = false;
                MessageBox.instance.Display(new MessageBox.Message(Step[2].messageTitle, Step[2].messagecontent, Step[2].timer));
            }
        }
    }
}
