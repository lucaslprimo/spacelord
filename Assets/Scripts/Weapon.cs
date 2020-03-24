using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootingPosition;
    public float shotRate;
    private float shotTime = 0f;

    public string weaponName;
    public int maxBullets;
    private GunBulletsController bulletsController;
  

    private void Start()
    {
        bulletsController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GunBulletsController>();
        bulletsController.loadWeaponUI(this);
    }

    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButton(0))
        {
            if(Time.time > shotTime)
            {
                Quaternion rotationCorrection = Quaternion.AngleAxis(-90, Vector3.forward);
                Instantiate(projectile, shootingPosition.position,transform.rotation * rotationCorrection);
                shotTime = Time.time + 1/shotRate;
                bulletsController.UpdateBullets(this);
            }
        }
    }
}
