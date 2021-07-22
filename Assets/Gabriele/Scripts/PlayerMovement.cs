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
    private Rigidbody2D rb2d;

    float moveH=0, moveV=0;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = speedRegular;
    }

    void FixedUpdate()
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
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");

        rb2d.velocity = (new Vector2(moveH, moveV)).normalized*speed;

        if(moveH ==0 && moveV == 0)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
    }
}
