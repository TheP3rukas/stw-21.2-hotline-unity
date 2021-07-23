using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public enum Weapons { None, Axe, Pistol};
    public Weapons currentWeapon;
    GameObject currentWeaponPrefab;
    private Animator anim;
    public GameObject axePrefab;
    public GameObject pistolPrefab;

    private Shoot shoot;

    void Start()
    {
        currentWeapon = Weapons.None;
        anim = GetComponent<Animator>();
        shoot = GetComponent<Shoot>();
    }


    void Update()
    {
        if(currentWeapon == Weapons.None)
        {
            anim.SetBool("axe", false);
            anim.SetBool("pistol", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropWeapon(currentWeaponPrefab);
        }

        if(currentWeapon == Weapons.Pistol)
        {
            if (!shoot.enabled)
            {
                shoot.enabled = true;
            }
            
        }
        else
        {
            shoot.enabled = false;
        }
    }

    public void PickupWeapon(string weaponTag)
    {
        if(currentWeapon!=Weapons.None)
        {
            DropWeapon(currentWeaponPrefab);
        }
        if (weaponTag == "axe")
        {
            currentWeapon = Weapons.Axe;
            currentWeaponPrefab = axePrefab;
            anim.SetBool("axe", true);
            anim.SetBool("pistol", false);
        }
        else if (weaponTag == "pistol")
        {
            currentWeapon = Weapons.Pistol;
            currentWeaponPrefab = pistolPrefab;
            anim.SetBool("pistol", true);
            anim.SetBool("axe", false);
        }
        
    }

    private void DropWeapon(GameObject prefab)
    {
        if (currentWeapon != Weapons.None)
        {
            currentWeapon = Weapons.None;
            currentWeaponPrefab = null;
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
