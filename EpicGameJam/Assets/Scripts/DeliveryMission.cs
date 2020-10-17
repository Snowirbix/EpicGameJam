using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryMission : MonoBehaviour
{
    public Transform[] deliverys;

    void Start()
    {
        FirstPersonController.instance.Q<Delivery>().targets = deliverys;
    }

}
