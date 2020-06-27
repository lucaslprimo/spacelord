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

    public float startDelay;
    private float startTime;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyAction", lifeTime);
        Instantiate(shotSound, transform.position, transform.rotation);

        startTime = Time.time + startDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime)
        {
            this.GetComponent<Collider2D>().enabled = true;
        }


        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    void DestroyAction()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Being>() != null) 
        { 
            collision.GetComponent<Being>().TakeDamage(damage);
            if (!massive)
            {
                DestroyAction();
            }
        }
    }




}
