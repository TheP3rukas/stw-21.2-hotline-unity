using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("pts", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
