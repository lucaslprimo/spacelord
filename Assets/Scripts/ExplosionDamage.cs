using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(transform.position, 8f);
        if (collider2D != null) { 
            foreach (Being victim in collider2D.GetComponents<Being>())
            {
                Debug.Log("Hit");
                victim.TakeDamage(5);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 8f);
    }
}
