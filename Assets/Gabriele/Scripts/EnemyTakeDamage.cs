using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;
    private RandomPatrol rp;

    public List<AudioSource> dieAxeSounds;
    public List<AudioSource> dieSounds;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider2D>();
        rp = GetComponent<RandomPatrol>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if(other.CompareTag("playerBullet"))
        {
            Die("deadPistol", dieSounds);
        }

        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<Animator>().GetBool("axe") && other.GetComponent<Animator>().GetBool("attack"))
            {
                Die("deadAxe", dieAxeSounds);
            }
        }
    }

    private void Die(string animation, List<AudioSource> audioList)
    {
        anim.SetBool(animation, true);
        coll.enabled = false;
        rp.enabled = false;
        int i = Random.Range(0, audioList.Count);
        audioList[i].Play();
    }
}
