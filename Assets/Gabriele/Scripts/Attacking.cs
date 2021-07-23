using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    private Animator anim;
    private WeaponManager weaponManager;

    public List<AudioSource> swingSounds;

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
        if (anim.GetBool("axe"))
        {
            int i = Random.Range(0, swingSounds.Count);
            swingSounds[i].Play();
        }
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("attack", false);
    }
}
