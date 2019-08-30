using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryMission : MonoBehaviour
{
    public Transform[] deliverys;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.GetComponent<Delivery>().targets = deliverys;
    }

}
