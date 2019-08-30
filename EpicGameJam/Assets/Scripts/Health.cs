using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 40;

    [ReadOnly]
    public float health;

    [ReadOnly]
    public bool pendingDeath = false;
    protected float deathTime;

    public Component[] disableComponents;

    public GameObject dying;


    private void Start ()
    {
        health = maxHealth;
    }

    public bool ChangeHealth (float value)
    {
        health += value;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health == 0)
        {
            Die();
            return true;
        }

        return false;
    }

    private void Update ()
    {
        if (pendingDeath)
        {
            float ratio = Mathf.Clamp01(Time.time - deathTime);

            transform.localScale = Vector3.one * (1-ratio);

            if (ratio == 1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Die ()
    {
        pendingDeath = true;
        deathTime = Time.time;

        Instantiate(dying, transform.position + Vector3.up, transform.rotation, transform);

        foreach (Component component in disableComponents)
        {
            if (component is Renderer) {
                (component as Renderer).enabled = false;
            } else if (component is Collider) {
                (component as Collider).enabled = false;
            } else if (component is Behaviour) {
                (component as Behaviour).enabled = false;
            }
        }
    }
}
