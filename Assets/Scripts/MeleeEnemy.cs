using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public int damage;
    public float attackSpeed;

    IEnumerator AttackRoutine()
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }

    public override void Attack()
    {
        if (Time.time >= attackTime)
        {
            StartCoroutine(AttackRoutine());
            attackTime = Time.time + 1/attackRate;
        }
        
    }
}
