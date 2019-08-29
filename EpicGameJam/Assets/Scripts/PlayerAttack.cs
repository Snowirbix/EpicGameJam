using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerAttack : MonoBehaviour
{
    public GameObject impact;
    protected BoxCollider box;

    protected List<Collider> colliders = new List<Collider>();

    private void Start ()
    {
        box = GetComponent<BoxCollider>();
    }

    public void Attack ()
    {
        foreach (Collider col in colliders)
        {
            if (col.tag == "Enemy")
            {
                col.GetComponent<Health>().ChangeHealth(-20);
                Instantiate(impact, col.transform.position + Vector3.up, col.transform.rotation, col.transform);
            }
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        Debug.Log("Enter " + other.name);
        colliders.Add(other);
    }

    private void OnTriggerExit (Collider other)
    {
        Debug.Log("Exit " + other.name);
        // TODO: check exit works when player dies
        colliders.Remove(other);
    }
}
