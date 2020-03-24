using System;
using UnityEngine;
using UnityEngine.UI;

public class GunBulletsController : MonoBehaviour
{
    public GameObject defaultWeapon;
    public Text bulletsLabel;
    public GameObject inifiniteLabel;
    public Text gunName;

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void UpdateBullets(Weapon weapon)
    {
        if (!weapon.weaponName.Equals(defaultWeapon.GetComponent<Weapon>().weaponName))
        {
            int bullets = Int32.Parse(bulletsLabel.text);
            bullets--;
            if (bullets == 0)
            {
                player.EquipeWeapon(defaultWeapon.GetComponent<Weapon>());
            }
            else
            {
                bulletsLabel.text = bullets.ToString();
            }
        }
    }

    public void loadWeaponUI(Weapon weapon)
    {
        if (weapon.weaponName.Equals(defaultWeapon.GetComponent<Weapon>().weaponName))
        {
            inifiniteLabel.SetActive(true);
            bulletsLabel.gameObject.SetActive(false);
        }
        else
        {
            inifiniteLabel.SetActive(false);
            bulletsLabel.gameObject.SetActive(true);
            bulletsLabel.text = weapon.maxBullets.ToString();
        }

        gunName.text = weapon.weaponName;
    }
}
