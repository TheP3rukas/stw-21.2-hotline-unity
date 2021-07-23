using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class BulletTimeScript : MonoBehaviour
{
    [HideInInspector]
    public float slowDownFactor = 0.05f;
    [HideInInspector]
    public float slowDownLength = 2f;

    public VolumeProfile volumeProfile;
    UnityEngine.Rendering.Universal.Vignette vignette;

    public bool timeIsSlow;

    private void Start()
    {
        volumeProfile.TryGet(out vignette);
        if(vignette != null)
        {
            vignette.intensity.value = 0.2f;
        }
        Time.fixedDeltaTime = slowDownFactor * 0.02f;
    }

    void BulletTime()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = slowDownFactor * 0.02f;
        vignette.intensity.value = 0.4f;
        timeIsSlow = true;
    }

    void BulletTimeUp()
    {
        Time.timeScale = 1f;
        vignette.intensity.value = 0.2f;
        timeIsSlow = false;
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
