using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    protected BowController bowCtrl;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void Fire (BowController bow)
    {
        bowCtrl = bow;
    }
}
