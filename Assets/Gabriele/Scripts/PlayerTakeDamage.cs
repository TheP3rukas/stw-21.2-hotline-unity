using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTakeDamage : MonoBehaviour
{
    public AudioSource deathSound;
    public GameObject deathPanel;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            deathSound.Play();
            deathPanel.SetActive(true);
            anim.enabled = false;
            Time.timeScale = 0;
        }
    }
}
