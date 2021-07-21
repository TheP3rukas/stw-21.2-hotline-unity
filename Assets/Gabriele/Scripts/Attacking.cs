using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    private Animator anim;
    private WeaponManager weaponManager;

    void Start()
    {
        anim = GetComponent<Animator>();
        weaponManager = GetComponent<WeaponManager>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackAnimation());
        }

    }

    IEnumerator AttackAnimation()
    {
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("attack", false);
    }
}
