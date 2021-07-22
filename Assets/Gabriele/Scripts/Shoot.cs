using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shootingEffect;
    public float destroyTime = 3f;

    private Transform shootingPoint;

    void Start()
    {
        shootingPoint = GameObject.FindGameObjectWithTag("playerShootingPoint").GetComponent<Transform>();
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
        GameObject clone = Instantiate(shootingEffect, shootingPoint.position, transform.rotation);
        GameObject.Destroy(clone, destroyTime);
    }
}
