using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class BulletTimeScript : MonoBehaviour
{
    public float slowDownFactor = 0.05f;
    public float slowDownLength = 2f;

    public VolumeProfile volumeProfile;
    UnityEngine.Rendering.Universal.Vignette vignette;

    private void Start()
    {
        volumeProfile.TryGet(out vignette);
        if(vignette != null)
        {
            vignette.intensity.value = 0.2f;
        }
    }

    void BulletTime()
    {
        Time.timeScale = slowDownFactor;
        vignette.intensity.value = 0.4f;
        Debug.Log("slowDOWN");
    }

    void BulletTimeUp()
    {
        Time.timeScale = 1f;
        vignette.intensity.value = 0.2f;
        Debug.Log("slowDOWN");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            BulletTime();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            BulletTimeUp();
        }
    }
}
