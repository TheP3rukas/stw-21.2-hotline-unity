using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTakeDamage : MonoBehaviour
{
    public AudioSource deathSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            deathSound.Play();
            //enable death panel
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
