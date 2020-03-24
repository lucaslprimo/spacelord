using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoss : MonoBehaviour
{
    public float dropChance;
    public GameObject[] drops;

    public float speed;
    public float lifeTime;

    public GameObject shotSound;
    public GameObject explosion;
    public GameObject explosionSound;
    public int damage;

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

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Being>().TakeDamage(damage);
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(explosionSound, transform.position, transform.rotation);
            DestroyAction();
        }

        if( collision.tag == "Projectile")
        {
            float randomNumber = Random.Range(0, 100);
            if (randomNumber < dropChance)
            {
                Instantiate(drops[Random.Range(0, drops.Length)], transform.position, new Quaternion());
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(explosionSound, transform.position, transform.rotation);
            DestroyAction();
        }

    }

}
