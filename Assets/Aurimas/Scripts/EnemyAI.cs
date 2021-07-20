using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
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

    [HideInInspector]
    public bool patroling;

    void Start()
    {
        seeker = GetComponent<Seeker>();

        patroling = true;
        waitTime = startWaitTime;
        randomPoint = Random.Range(0, patrolPoints.Length);

        InvokeRepeating("UpdatePath", 0f, 2f);

    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(transform.position, patrolPoints[randomPoint].position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (patroling)
        {
            Patrol();
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
