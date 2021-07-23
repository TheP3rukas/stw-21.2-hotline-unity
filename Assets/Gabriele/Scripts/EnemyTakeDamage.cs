using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if(other.CompareTag("playerBullet"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<Animator>().GetBool("axe") && other.GetComponent<Animator>().GetBool("attack"))
            {
                Destroy(gameObject);
            }
        }
    }
}
