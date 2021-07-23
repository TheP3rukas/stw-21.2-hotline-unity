using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float destroyTime = 3f;
    private ParticleSystem muzzleFlash;

    private Transform shootingPoint;

    void Start()
    {
        shootingPoint = GameObject.FindGameObjectWithTag("playerShootingPoint").GetComponent<Transform>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnShoot();
        }
    }

    void OnShoot()
    {
        GameObject clone = Instantiate(bullet, shootingPoint.position, transform.rotation);
        muzzleFlash.Play();
        Destroy(clone, destroyTime);
    }
}
