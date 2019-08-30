using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plug : InteractableScript
{
    protected bool once = true;

    void Start()
    {
        gameObject.tag = "Untagged";
    }
    void Update()
    {
        if(Tutorial.instance.secondStep && once)
        {
            gameObject.tag = "interactable";
            PlayerController.instance.transform.GetComponent<Delivery>().enabled = true;
            PlayerController.instance.transform.GetComponent<Delivery>().targets = new Transform[]{gameObject.transform};
            once = false;
        }
    }
    override public void Interact()
    {
        //InformationBox.instance.Display(new InformationBox.Information(new []{"As you put your Fingers into the plug, you died after being electrocuted"}));
        MessageBox.instance.Display(new MessageBox.Message("Bzzzzztt",new []{new MessageBox.Sentence("As you put your Fingers into the plug, you died after being electrocuted", 4)}));
        Tutorial.instance.thirdStep = true;
    }
}
