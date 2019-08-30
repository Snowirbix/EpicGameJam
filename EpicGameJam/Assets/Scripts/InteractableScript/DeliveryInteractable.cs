using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryInteractable : InteractableScript
{
    protected Delivery delivery;
    // Start is called before the first frame update
    void Start()
    {
        delivery = PlayerController.instance.GetComponent<Delivery>();
    }

    // Update is called once per frame
    void Update()
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

    public override void Interact()
    {
        MessageBox.instance.Display(new MessageBox.Message(messageObject.messageTitle,messageObject.messageContent));
        delivery.Next();
        PlayerController.instance.HideBox();
    }
}
