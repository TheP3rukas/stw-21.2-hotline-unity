using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPitch : MonoBehaviour
{
    BulletTimeScript bulletTime;

    public AudioSource music;
    public AudioSource hearthbeat;

    private void Start()
    {
        bulletTime = GetComponent<BulletTimeScript>();
    }

    private void Update()
    {
        if(bulletTime.timeIsSlow)
        {
            music.pitch = 0.75f;
            music.volume = 0.05f;
            hearthbeat.enabled = true;
            hearthbeat.loop = true;
        } 
        else if(!bulletTime.timeIsSlow)
        {
            music.pitch = 1f;
            music.volume = 0.1f;
            hearthbeat.enabled = false;
            hearthbeat.loop = false;
        }
    }
}
