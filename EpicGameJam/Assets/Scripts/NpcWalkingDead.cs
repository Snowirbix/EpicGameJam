using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWalkingDead : MonoBehaviour
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
    }
}
