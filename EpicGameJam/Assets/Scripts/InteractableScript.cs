using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    public MessageObject messageObject;
    void Start()
    {
        if(transform.tag != "interactable")
        {
            Debug.LogError("the gameObject must be tagged 'interactable'");
        }
    }

    public virtual void Interact()
    {
        MessageBox.instance.Display(new MessageBox.Message(messageObject.messageTitle , messageObject.messageContent));
    }
}
