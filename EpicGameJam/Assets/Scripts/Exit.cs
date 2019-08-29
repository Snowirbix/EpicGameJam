using UnityEngine;

public class Exit : MonoBehaviour
{
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
