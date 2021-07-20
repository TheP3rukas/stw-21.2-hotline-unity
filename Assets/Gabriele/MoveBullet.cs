using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveBullet : MonoBehaviour
{
    public float shootingSpeed = 10f;

    void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime*shootingSpeed);
    }
}
