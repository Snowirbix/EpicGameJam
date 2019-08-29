using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter (Collision other)
    {
        if (other.collider.tag == "Player")
        {
            PlayerController.instance.ChangeHealth(-20);
        }
        Destroy(gameObject);
    }
}