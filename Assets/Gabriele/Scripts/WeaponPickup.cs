using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.CompareTag("Player") && Input.GetMouseButtonDown(1))
        {
            coll.GetComponent<WeaponManager>().PickupWeapon(gameObject.tag);
            Destroy(gameObject);
        }
    }
}
