using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    enum Weapons { None, Axe, Pistol};
    Weapons currentWeapon;
    GameObject currentWeaponPrefab;
    private Animator anim;
    public GameObject axePrefab;
    public GameObject pistolPrefab;

    void Start()
    {
        currentWeapon = Weapons.None;
        anim = GetComponent<Animator>();
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
