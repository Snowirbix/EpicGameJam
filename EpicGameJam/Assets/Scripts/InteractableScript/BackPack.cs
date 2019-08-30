using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : InteractableScript
{
    protected bool once = true;
    void Start()
    {
        gameObject.tag = "Untagged";
    }
     public override void Interact()
    {
        PlayerController.instance.backpack.SetActive(true);
        PlayerController.instance.HideBox();
        Tutorial.instance.fifthStep = true;
        Destroy(gameObject);
    }

    void Update()
    {
        if(Tutorial.instance.fourthStep == true && once)
        {
            gameObject.tag = "interactable";
            PlayerController.instance.transform.GetComponent<Delivery>().targets = new Transform[]{gameObject.transform};
            once = false;
        }
    }
}
