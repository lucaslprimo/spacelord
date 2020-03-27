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
    public FloatingJoystick mobileFireControl;

    Vector2 direction;

    private bool IsMobile
    {
        get
        {
            #if UNITY_ANDROID
                            return true;
            #else
                        return false;
            #endif
        }
    }

    private void Start()
    {
        bulletsController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GunBulletsController>();
        bulletsController.loadWeaponUI(this);

        mobileFireControl.enabled = true;
    }

    void Update()
    {
        if (IsMobile)
        {
            Vector2 move = new Vector2(mobileFireControl.Horizontal, mobileFireControl.Vertical);
            if (move != Vector2.zero)
            {
                direction = mobileFireControl.Direction;
            }
        }
        else
        {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }

        UpdateWeaponRotation(direction);

        bool isShooting;
        if (!IsMobile)
        {
            isShooting = Input.GetMouseButton(0);
        }
        else
        {
            Vector2 move = new Vector2(mobileFireControl.Horizontal, mobileFireControl.Vertical);
            isShooting = move != Vector2.zero;           
        }

        if (isShooting)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > shotTime)
        {
            Quaternion rotationCorrection = Quaternion.AngleAxis(-90, Vector3.forward);
            Instantiate(projectile, shootingPosition.position, transform.rotation * rotationCorrection);
            shotTime = Time.time + 1 / shotRate;
            bulletsController.UpdateBullets(this);
        }
    }

    private void UpdateWeaponRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
}
