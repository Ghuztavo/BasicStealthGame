using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

// monster that just moves between waypoints
public class Monster2 : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public List<Transform> wayPoints;
    private int currentWaypoint = 0;
    [SerializeField] private float stayTime = 2.0f;
    private float stayTimer = 0.0f;
    private bool waiting = false;

    Rigidbody rb;

    public GameObject monster;
    Animator animator;

    void Start()
    {
        navMeshAgent.SetDestination(wayPoints[0].position);
        rb = GetComponent<Rigidbody>();
        if (gameObject != null)
        {
            animator = monster.GetComponent<Animator>();
        }

        stayTimer = stayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {
            //when it reaches the destination, i will move to the next one
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                waiting = true;
                stayTimer = stayTime;
            }
        }
        else
        {
            stayTimer -= Time.deltaTime;
            if (stayTimer <= 0)
            {
                waiting = false;
                MoveToNextWaypoint();
            }
        }

        //animation
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

    }

    private void MoveToNextWaypoint()
    {
        currentWaypoint = (currentWaypoint + 1) % wayPoints.Count;
        navMeshAgent.SetDestination(wayPoints[currentWaypoint].position);
    }

}

