using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 40;

    [HideInInspector]
    public float health;


    private void Start ()
    {
        health = maxHealth;
    }

    public void ChangeHealth (float value)
    {
        health += value;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health == 0)
        {
            Die();
        }
    }

    public void Die ()
    {
        Debug.Log("Dead");
    }
}
