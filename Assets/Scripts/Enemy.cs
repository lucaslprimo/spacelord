using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Being
{
    public float attackRate;
    protected float attackTime;

    public float dropChance;
    public GameObject[] drops;

    [HideInInspector]
    public Transform player;

    public float stopDistance;

    public GameObject explosion;

    public GameObject explosionSound;

    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Update()
    {
        if (player != null)
        {
            if(!Move())
            {
                Attack();
            }
        }
    }

    public virtual bool Move()
    {
        if (Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    public abstract void Attack();

    public override void Die()
    {
        base.Die();

        Instantiate(explosionSound, transform.position, transform.rotation);
        Instantiate(explosion, transform.position, transform.rotation);

        float randomNumber = Random.Range(0, 100);
        if(randomNumber < dropChance)
        {
            Instantiate(drops[Random.Range(0, drops.Length)], transform.position, transform.rotation);
        }
    }


}
