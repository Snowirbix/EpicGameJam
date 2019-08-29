using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plug : InteractableScript
{
    override public void Interact()
    {
        //InformationBox.instance.Display(new InformationBox.Information(new []{"As you put your Fingers into the plug, you died after being electrocuted"}));
        MessageBox.instance.Display(new MessageBox.Message("Bzzzzztt",new []{new MessageBox.Sentence("As you put your Fingers into the plug, you died after being electrocuted", 4)}));
        Tutorial.instance.fourthStep = true;
    }
}
