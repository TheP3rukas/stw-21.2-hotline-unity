using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RandomPatrol : MonoBehaviour
{
    //Patrolling variables
    public Transform patrolPoint;
    private float waitTime;
    public float startWaitTime;
    //private int randomPoint;
    public float speed = 200f;
    public float nextWaypointDistance = 0.2f;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Path path;
    Seeker seeker;
    public bool patroling = true;

    //Player transform
    public Transform target;
    public GameObject gameManager;
    public Collider colliderBond;

    EnemyAttack enemyAttScript;
    FOV fovScript;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        fovScript = GetComponent<FOV>();
        enemyAttScript = GetComponent<EnemyAttack>();

        waitTime = startWaitTime;
        patrolPoint.position = new Vector2(Random.Range(colliderBond.bounds.min.x, colliderBond.bounds.max.x), Random.Range(colliderBond.bounds.min.y, colliderBond.bounds.max.y));
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void Awake()
    {
        patroling = true;
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && patroling)
        {
            seeker.StartPath(transform.position, patrolPoint.position, OnPathComplete);
        }
        else if (seeker.IsDone() && !patroling)
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }

    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void Update()
    {
        if (fovScript.visibleTargets.Count == 0)
        {
            patroling = true;
            speed = 5f;
        }
        else if (fovScript.visibleTargets.Count > 0)
        {
            patroling = false;
        }

        if (patroling)
        {
            Patrol();
        }
        if (!patroling)
        {
            Attacking();
        }
    }

    void Attacking()
    {
        enemyAttScript.canShoot = true;
        patrolPoint.position = target.position;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;
        
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (distance < 2f)
        {
            path = null;
            speed = 0;
        }
    }

    void Patrol()
    {
        enemyAttScript.canShoot = false;

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (waitTime <= 0)
            {
                patrolPoint.position = new Vector2(Random.Range(colliderBond.bounds.min.x, colliderBond.bounds.max.x), Random.Range(colliderBond.bounds.min.y, colliderBond.bounds.max.y));
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
        transform.rotation = Quaternion.LookRotation(Vector3.forward, path.vectorPath[currentWaypoint] - transform.position);
    }
}
