using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PatrolAI : MonoBehaviour
{
    Path path;
    Seeker seeker;

    public float walkSpeed;
    private float waitTime;
    public float startWaitTime;

    [HideInInspector]
    public bool patroling;

    public Transform[] patrolPoints;
    private int randomPoint;

    //public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        patroling = true;
        waitTime = startWaitTime;
        randomPoint = Random.Range(0, patrolPoints.Length);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(patroling)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[randomPoint].position, walkSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, patrolPoints[randomPoint].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomPoint = Random.Range(0, patrolPoints.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        transform.rotation = Quaternion.LookRotation(Vector3.forward, patrolPoints[randomPoint].position - transform.position);
    }
}
