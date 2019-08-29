using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    public Options options;
    public MessageObject[] StepQ;

    public MessageObject[] StepA;

    private MessageObject[] Step;

    public bool firstStep, secondStep, thirdStep, fourthStep = false;
    
    public Image fade;

    private Vector2 axes;

    bool left,right = false;

    //bool fadeInComplete = false;
    MessageBox.Message msg;

    Vector3 initialPosition = new Vector3(0,0.09f,-4f);
    void Start()
    {
        instance = this;

        PlayerController.instance.transform.SetPositionAndRotation(initialPosition, Quaternion.identity);

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

        if(firstStep)
        {
            StartCoroutine(FadeOut());
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

        if(fourthStep)
        {
            fourthStep = false;
            StopAllCoroutines();
            StartCoroutine(FadeInAndOut());
        }
    }

    protected IEnumerator FadeOut ()
    {
        while (fade.color.a > 0f)
        {
            fade.color = new Color(fade.color.r, fade.color.g , fade.color.b , fade.color.a - Time.deltaTime);
            yield return null;
        }
        fade.gameObject.SetActive(false);
    }

    protected IEnumerator FadeIn ()
    {
        fade.gameObject.SetActive(true);
        while (fade.color.a < 1f)
        {
            fade.color = new Color(fade.color.r, fade.color.g , fade.color.b , fade.color.a + Time.deltaTime);
            yield return null;
        }
    }

    protected IEnumerator FadeInAndOut ()
    {
        fade.gameObject.SetActive(true);
        while (fade.color.a < 1f)
        {
            fade.color = new Color(fade.color.r, fade.color.g , fade.color.b , fade.color.a + Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        PlayerController.instance.transform.SetPositionAndRotation(initialPosition, Quaternion.identity);
        while (fade.color.a > 0f)
        {
            fade.color = new Color(fade.color.r, fade.color.g , fade.color.b , fade.color.a - Time.deltaTime);
            yield return null;
        }
        fade.gameObject.SetActive(false);
    }
}
