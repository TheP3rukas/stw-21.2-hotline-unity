using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private int enemiesCount;
    public GameObject winPanel;
    public GameObject teleport;

    void Start()
    {
        enemiesCount = transform.childCount;
    }

    
    void Update()
    {
        if (enemiesCount <= 0 && SceneManager.GetActiveScene().buildIndex==2)
        {
            winPanel.SetActive(true);
        }
        else if(enemiesCount <= 0 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            winPanel.SetActive(true);
            teleport.SetActive(true);
        }
    }

    public void OnEnemyShot()
    {
        enemiesCount--;
    }
}
