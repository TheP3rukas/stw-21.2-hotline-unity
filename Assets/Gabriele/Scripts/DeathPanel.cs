using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DeathPanel : MonoBehaviour
{
    public void RestartLevel()
    {
        PlayerPrefs.SetInt("pts", 0);
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        PlayerPrefs.SetInt("pts", 0);
        SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
    }
}
