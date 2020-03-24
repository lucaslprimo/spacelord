using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Enemy
{
    public GameObject projectile;
    public Transform shootingPosition;

    public override void Attack()
    {
        Vector2 direction = transform.position - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
       
        if (Time.time > attackTime)
        {
            Quaternion rotationCorrection = Quaternion.AngleAxis(90, Vector3.forward);
            Instantiate(projectile, shootingPosition.position, rotation * rotationCorrection);
            attackTime = Time.time + 1 / attackRate;
        }
    }
}
