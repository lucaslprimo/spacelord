﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLife : MonoBehaviour
{
    public int amount;
    public GameObject sound;
    public float lifetime;
    private  float deadTime;
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        deadTime = Time.time + lifetime;
    }

    private void Update()
    {
        if(Time.time >= deadTime)
        {
            Destroy(gameObject);
        }
        else
        {
            if(deadTime - Time.time <= 2)
            {
                anim.SetBool("vanishing", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().Heal(amount);
            Instantiate(sound, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
