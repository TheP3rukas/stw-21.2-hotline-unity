using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shootingEffect;
    public float destroyTime = 3f;

    void Start()
    {
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
        GameObject clone = Instantiate(shootingEffect, transform.position, transform.rotation);
        GameObject.Destroy(clone, destroyTime);
    }
}
