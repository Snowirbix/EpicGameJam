using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWalkingDead : MonoBehaviour
{
    protected Delivery delivery;
    void Start()
    {
        delivery = PlayerController.instance.GetComponent<Delivery>();
    }

    // Update is called once per frame
    void Update()
    {
        if(delivery.currentTarget == delivery.targets.Length)
        {
            gameObject.SetActive(false);
        }
    }
}
