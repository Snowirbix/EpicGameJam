using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class ZombieAttack : MonoBehaviour
{
    public MeshRenderer[] renderers;
    protected BoxCollider box;

    protected List<Collider> colliders = new List<Collider>();

    private void Start ()
    {
        box = GetComponent<BoxCollider>();
    }

    public void Render (bool show)
    {
        foreach (MeshRenderer rend in renderers)
        {
            rend.enabled = show;
        }
    }

    public void Attack ()
    {
        foreach (Collider col in colliders)
        {
            if (col.tag == "Player")
            {
                col.GetComponent<PlayerController>().ChangeHealth(-20);
            }
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        colliders.Add(other);
    }

    private void OnTriggerExit (Collider other)
    {
        // TODO: check exit works when player dies
        colliders.Remove(other);
    }
}
