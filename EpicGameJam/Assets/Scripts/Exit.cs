using UnityEngine;

public class Exit : MonoBehaviour
{
    protected bool once = true;
    void Update()
    {
        if(Tutorial.instance.fifthStep && once)
        {
            PlayerController.instance.transform.GetComponent<Delivery>().targets = new Transform[]{gameObject.transform};
            once = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Tutorial.instance.fifthStep)
            {
                Tutorial.instance.EndTutorial();
            }
        }
    }
}
