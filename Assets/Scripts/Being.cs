using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being : MonoBehaviour
{
    public float speed;
    internal int health;
    public int maxHealth;

    public virtual void Start() {
        health = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        UpdateHealth(health - damage);

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void UpdateHealth(int newHealth)
    {
        if (newHealth > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health = newHealth;
        }
    }
}
