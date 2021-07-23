using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DeathPanel : MonoBehaviour
{
    public VolumeProfile volumeProfile;
    UnityEngine.Rendering.Universal.Vignette vignette;

    private void Start()
    {
        volumeProfile.TryGet(out vignette);
        if (vignette != null)
        {
            vignette.intensity.value = 1f;
        }
    }
}
