using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    public GameObject impact;

    private void OnCollisionEnter (Collision other)
    {
        if (other.collider.tag == "Player")
        {
            PlayerController.instance.ChangeHealth(-20);
            Instantiate(impact, PlayerController.instance.RayCaster.position, PlayerController.instance.RayCaster.rotation, PlayerController.instance.RayCaster);
        }
        Destroy(gameObject);
    }
}