using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    public Weapon weaponToEquip;
    public GameObject sound;
    public float lifetime;
    private float deadTime;

    private void Start()
    {
        deadTime = Time.time + lifetime;
    }

    private void Update()
    {
        if (Time.time >= deadTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().EquipeWeapon(weaponToEquip);
            Instantiate(sound, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
