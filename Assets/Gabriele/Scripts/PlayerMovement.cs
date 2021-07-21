using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedRegular = 5.0f;
    public float speed;
    public float speedFaster = 100f;
    private bool moving = false;
    private Animator anim;
    public BulletTimeScript bulletTime;

    private void Start()
    {
        anim = GetComponent<Animator>();
        speed = speedRegular;
    }

    void Update()
    {
        Movement();
        anim.SetBool("moving", moving);

        if (bulletTime.timeIsSlow)
        {
            speed = speedFaster;
        }
        else
        {
            speed = speedRegular;
        }
    }

    void Movement()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            moving = true;
        }

        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            moving = false;
        }
    }
}
