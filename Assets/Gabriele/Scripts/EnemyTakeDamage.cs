using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;
    private RandomPatrol rp;
    private Points points;

    public List<AudioSource> dieAxeSounds;
    public List<AudioSource> dieSounds;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider2D>();
        rp = GetComponent<RandomPatrol>();
        points = GameObject.FindGameObjectWithTag("gm").GetComponent<Points>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if(other.CompareTag("playerBullet"))
        {
            Die("deadPistol", dieSounds, Random.Range(20,50));
        }

        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<Animator>().GetBool("axe") && other.GetComponent<Animator>().GetBool("attack"))
            {
                Die("deadAxe", dieAxeSounds, Random.Range(50, 100));
            }
        }
    }

    private void Die(string animation, List<AudioSource> audioList, int pts)
    {
        anim.SetBool(animation, true);
        points.OnAddPoints(pts);
        coll.enabled = false;
        rp.enabled = false;
        int i = Random.Range(0, audioList.Count);
        audioList[i].Play();
    }
}
