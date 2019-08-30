using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class ZombieAttack : MonoBehaviour
{
    public MeshRenderer[] renderers;
    public GameObject impact;
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
        for (int i = colliders.Count-1; i >= 0; i--)
        {
            Collider col = colliders[i];

            if (col.tag == "Player" && !col.isTrigger)
            {
                if (PlayerController.instance.ChangeHealth(-20))
                {
                    colliders.RemoveAt(i);
                }
                else
                {
                    Instantiate(impact, PlayerController.instance.RayCaster.position, PlayerController.instance.RayCaster.rotation, PlayerController.instance.RayCaster);
                }
            }
        }
    }

    private void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Player" && !col.isTrigger)
        {
            colliders.Add(col);
        }
    }

    private void OnTriggerExit (Collider col)
    {
        if (col.tag == "Player" && !col.isTrigger)
        {
            colliders.Remove(col);
        }
    }
}
