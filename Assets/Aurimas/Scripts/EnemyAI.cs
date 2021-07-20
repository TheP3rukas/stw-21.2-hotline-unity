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

    [HideInInspector]
    public bool isAttacking;
    //[HideInInspector]
    public bool patroling;

    //Player transform
    public Transform target;

    FOV fovScript;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        fovScript = GetComponent<FOV>();

        patroling = true;
        waitTime = startWaitTime;
        randomPoint = Random.Range(0, patrolPoints.Length);

        InvokeRepeating("UpdatePath", 0f, .5f);

    }

    void UpdatePath()
    {
        if(seeker.IsDone() && patroling)
        {
            seeker.StartPath(transform.position, patrolPoints[randomPoint].position, OnPathComplete);
        }
        else if(!patroling)
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
        }

        if(!patroling)
        {
            Attacking();
        }

        if(fovScript.visibleTargets.Count == 0)
        {
            patroling = true;
        }
        else if(fovScript.visibleTargets.Count > 0)
        {
            patroling = false;
        }
    }

    void Attacking()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance < 5f)
        {
            currentWaypoint = 0;
            Debug.Log("I am atttacking");
        }
    }

    void Patrol()
    {
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
