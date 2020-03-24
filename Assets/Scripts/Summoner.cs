using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Ranged
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 targetPosition;

    public float summonColdown;
    private float summonTime;

    public Enemy enemyToSummon;

    private Animator anim;

    public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        anim = GetComponent<Animator>();
        targetPosition = new Vector2(randomX, randomY);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }


    public override bool Move()
    {
        if (Vector2.Distance(transform.position, targetPosition) > .5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Attack()
    {
        if(Vector2.Distance(transform.position, player.position) < stopDistance)
        {
            base.Attack();
        }
       
        Summon();
    }


    public void Summon()
    {
        if(Time.time >= summonTime) {
            anim.SetTrigger("summon");
            Instantiate(enemyToSummon, transform.position, transform.rotation);
            summonTime = Time.time + summonColdown;
        }   
    }
}
