using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerAttack : MonoBehaviour
{
    public GameObject impact;
    protected BoxCollider box;

    protected List<Collider> colliders = new List<Collider>();

    protected List<Collider> hit = new List<Collider>();

    protected bool isAttacking = false;

    private void Start ()
    {
        box = GetComponent<BoxCollider>();
    }

    public void Attack ()
    {
        hit.Clear();
        isAttacking = true;

        for (int i = colliders.Count-1; i >= 0; i--)
        {
            Collider col = colliders[i];

            hit.Add(col);
            if (col.GetComponent<Health>().ChangeHealth(-20))
            {
                colliders.RemoveAt(i);
            }
            Instantiate(impact, col.transform.position + Vector3.up, col.transform.rotation, col.transform);
        }
    }

    public void StopAttack ()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Enemy")
        {
            // if we're attacking and we didn't hit it already
            if (isAttacking && !hit.Contains(col))
            {
                hit.Add(col);
                if (col.GetComponent<Health>().ChangeHealth(-20))
                {
                    // don't add
                }
                else
                {
                    colliders.Add(col);
                }
                Instantiate(impact, col.transform.position + Vector3.up, col.transform.rotation, col.transform);
            }
            else
            {
                colliders.Add(col);
            }
        }
    }

    private void OnTriggerExit (Collider col)
    {
        if (col.tag == "Enemy")
        {
            colliders.Remove(col);
        }
    }
}
