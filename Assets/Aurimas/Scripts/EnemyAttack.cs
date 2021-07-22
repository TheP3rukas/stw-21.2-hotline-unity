using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public ParticleSystem shooting;
    [HideInInspector]
    public bool canShoot;
    private bool isShooting;

    private float newFireTime;
    private float stopFireTime;
    public float fireRate;
    private bool attackLoopStarted;

    public GameObject bullet;
    public Transform bulletEmitt;
    public float bulletSpeed;

    void Update()
    {
        if (canShoot)
        {
            AttackTimer();
        }
    }

    void AttackTimer()
    {
        if (!attackLoopStarted)
        {
            attackLoopStarted = true;
            newFireTime = Time.time + (1 / fireRate);
            stopFireTime = Time.time + (1 / fireRate / 4f);
            Shoot();
        }
        if (attackLoopStarted)
        {
            if (Time.time >= newFireTime)
            {
                attackLoopStarted = false;
            }
            else if (Time.time >= stopFireTime && isShooting == true)
            {
                StopShoot();
            }
        }
    }

    private void Shoot()
    {
        isShooting = true;
        GameObject tempBullet;
        tempBullet = Instantiate(bullet, bulletEmitt.transform.position, bulletEmitt.transform.rotation) as GameObject;
        Rigidbody2D tempRB;
        tempRB = tempBullet.GetComponent<Rigidbody2D>();
        tempRB.AddForce(bulletEmitt.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(tempBullet, 2f);
        //playerScript.playerHealth -= 1;
        //audioSourceShooting.Play();
        shooting.Play();
    }

    private void StopShoot()
    {
        isShooting = false;
        // audioSourceReload.Play();
        shooting.Stop();
    }
}
