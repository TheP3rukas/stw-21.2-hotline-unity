using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    //Patrolling variables
    public Transform[] patrolPoints;
    private float waitTime;
    public float startWaitTime;
    private int randomPoint;
    public float speed = 200f;
    public float nextWaypointDistance = 0.2f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;

    //attack variables
    public ParticleSystem shooting;
    private bool canShoot;
    private float newFireTime;
    private float stopFireTime;
    public float fireRate;
    private bool attachLoopStarted;
    private bool isShooting;

    [HideInInspector]
    public bool isAttacking;
    //[HideInInspector]
    public bool patroling = true;

    //Player transform
    public Transform target;

    FOV fovScript;

    //shooting
    public GameObject bullet;
    public Transform bulletEmitt;
    public float bulletSpeed;

    private Quaternion lastRotation;

    public GameObject gameManager;
    BulletTimeScript bulletTimer;

    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        bulletTimer = gameManager.GetComponent<BulletTimeScript>();
        seeker = GetComponent<Seeker>();
        fovScript = GetComponent<FOV>();

        waitTime = startWaitTime;
        randomPoint = Random.Range(0, patrolPoints.Length);

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void Awake()
    {
        patroling = true;
    }

    void UpdatePath()
    {
        if(seeker.IsDone() && patroling)
        {
            seeker.StartPath(transform.position, patrolPoints[randomPoint].position, OnPathComplete);
        }
        else if(seeker.IsDone() && !patroling)
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
        
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void Update()
    {
        if (patroling)
        {
            Patrol();
        } else if(!patroling)
        {
            Attacking();
        }

        if(fovScript.visibleTargets.Count == 0)
        {
            patroling = true;
            speed = 5f;
        }
        else if(fovScript.visibleTargets.Count > 0)
        {
            patroling = false;
        }

        if(canShoot)
        {
            AttackTimer();
        }

        /*if(bulletTimer.timeIsSlow)
        {
            speed = speed / 20f;
            shooting.playbackSpeed = 0.05f;
            bulletSpeed = bulletSpeed / 20f;
        }
        else
        {
            speed = 5;
            shooting.playbackSpeed = 1f;
        }*/


        anim.SetBool("isShooting", isShooting);

    }

    void AttackTimer()
    {
        if(!attachLoopStarted)
        {
            attachLoopStarted = true;
            newFireTime = Time.time + (1 / fireRate);
            stopFireTime = Time.time + (1 / fireRate / 4f);
            Shoot();
        }
        if (attachLoopStarted)
        {
            if (Time.time >= newFireTime)
            {
                attachLoopStarted = false;
            }
            else if (Time.time >= stopFireTime && isShooting == true)
            {
                StopShoot();
            }
        }
    }

    private void Shoot()
    {
        isShooting = true;
        GameObject tempBullet;
        tempBullet = Instantiate(bullet, bulletEmitt.transform.position, bulletEmitt.transform.rotation) as GameObject;
        Rigidbody2D tempRB;
        tempRB = tempBullet.GetComponent<Rigidbody2D>();
        tempRB.AddForce(bulletEmitt.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(tempBullet, 2f);
        //playerScript.playerHealth -= 1;
        //audioSourceShooting.Play();
        shooting.Play();
    }

    private void StopShoot()
    {
        isShooting = false;
       // audioSourceReload.Play();
        shooting.Stop();
    }

    void Attacking()
    {
        canShoot = true;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);

        /*Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;
        */
        float distance = Vector2.Distance(transform.position, target.position);

       /* if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }*/
        if (distance < 5f)
        {
            path = null;
            speed = 0;
        }
    }

    void Patrol()
    {

        canShoot = false;

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (waitTime <= 0)
            {
                randomPoint = Random.Range(0, patrolPoints.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //rotation
        transform.rotation = Quaternion.LookRotation(Vector3.forward, path.vectorPath[currentWaypoint] - transform.position);    
    }
}
