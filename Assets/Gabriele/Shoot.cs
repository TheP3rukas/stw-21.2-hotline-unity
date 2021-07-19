using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    ParticleSystem shootingEffect;

    void Start()
    {
        shootingEffect = GetComponentInChildren<ParticleSystem>();
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
        shootingEffect.Play();
    }
}
