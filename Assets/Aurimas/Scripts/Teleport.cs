using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    private bool playerDetected;
    public Transform doorPos;
    public float width;
    public float height;
    public LayerMask whatIsPlayer;
    public GameObject UIText;

    public string SceneName;

    private void Start()
    {
        UIText.SetActive(false);
    }
    private void Update()
    {
        playerDetected = Physics2D.OverlapBox(doorPos.position, new Vector2(width, height), 0, whatIsPlayer);
        if(playerDetected == true)
        {
            UIText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                Load();
            }
        }
        else
        {
            UIText.SetActive(false);
        }
    }
    public void Load()
    {
        SceneManager.LoadScene(SceneName);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(doorPos.position, new Vector3(width, height, 1));
    }
}
