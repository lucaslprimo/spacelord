using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    public GameObject explosion;
    public GameObject shotSound;
    public int damage;
    public bool massive;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyAction", lifeTime);
        Instantiate(shotSound, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    void DestroyAction()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            if (!massive)
            {
                DestroyAction();
            }
        } else if( collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().TakeDamage(damage);
            DestroyAction();
        }
    }




}
