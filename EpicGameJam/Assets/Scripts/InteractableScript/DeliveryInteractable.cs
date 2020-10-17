using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryInteractable : InteractableScript
{
    protected Delivery delivery;

    void Start()
    {
        delivery = FirstPersonController.instance.Q<Delivery>();
    }

    void Update()
    {
        if(delivery.currentTarget == delivery.targets.Length)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if(delivery.targets[delivery.currentTarget] == gameObject.transform)
            {
                gameObject.tag = "interactable";
            }
            else
            {
                gameObject.tag = "Untagged";
            }
        }
        
    }

    public override void Interact()
    {
        MessageBox.instance.Display(new MessageBox.Message(messageObject.messageTitle, messageObject.messageContent));
        FirstPersonController.instance.HideBox();
        delivery.Next();
    }
}
