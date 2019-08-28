using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Options options;
    public MessageObject[] StepQ;

    public MessageObject[] StepA;

    private MessageObject[] Step;

    bool firstStep, secondStep, thirdStep = false;
    
    public Image fade;

    private Vector2 axes;

    bool left,right = false;

    MessageBox.Message msg;

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

        msg = new MessageBox.Message(Step[0].messageTitle, Step[0].messageContent);
        MessageBox.instance.Display(msg);
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
                msg.sentences[1].Next();
                msg = new MessageBox.Message(Step[1].messageTitle, Step[1].messageContent);
                MessageBox.instance.Display(msg);
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

            if(right && left)
            {
                secondStep = false;
                msg.sentences[2].Next();
                thirdStep = true;
            }
        }

        if (thirdStep)
        {
            if(PlayerController.instance.axes.y == -1)
            {
                thirdStep = false;
                msg.sentences[3].Next();
                msg = new MessageBox.Message(Step[2].messageTitle, Step[2].messageContent);
                MessageBox.instance.Display(msg);
            }
        }
    }
}
