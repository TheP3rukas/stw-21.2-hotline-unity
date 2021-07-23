using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateGif : MonoBehaviour
{
    public Sprite[] animatedImages;
    public SpriteRenderer animateImageObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animateImageObj.sprite = animatedImages [(int)(Time.time*10)%animatedImages.Length];
    }
}
