using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

// monster that chases player when it sees them
public class Monster3 : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform chaseTarget;

    public List<Transform> wayPoints;
    private int currentWaypoint = 0;
    [SerializeField] private float stayTime = 2.0f;
    private float stayTimer = 0.0f;
    private bool waiting = false;

    private M3AIState state;
    [SerializeField] private float chaseTime = 5.0f;
    private float chaseTimer = 0.0f;
    [SerializeField] private float wanderSpeed = 3.0f;
    [SerializeField] private float chaseSpeed = 5.0f;
    private bool playerInSight = false;

    Rigidbody rb;

    public GameObject monster;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (gameObject != null)
        {
            animator = monster.GetComponent<Animator>();
        }

        navMeshAgent.SetDestination(wayPoints[0].position);
        state = M3AIState.Wander;
        stayTimer = stayTime;
        chaseTimer = chaseTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case M3AIState.Wander:
                HandleWander();
                break;
            case M3AIState.Investigate:
                HandleInvestigate();
                break;
            case M3AIState.Chase:
                HandleChase();
                break;
        }

        //animation
        if (navMeshAgent.velocity.magnitude >= 0.1)
        {
            if (state == M3AIState.Chase)
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Running", true);
                animator.SetBool("Inspecting", false);
            }
            else
            {
                animator.SetBool("Walking", true);
                animator.SetBool("Running", false);
                animator.SetBool("Inspecting", false);
            }
        }
        else
        {
            if (state == M3AIState.Investigate)
            {
                animator.SetBool("Inspecting", true);
                animator.SetBool("Walking", false);
                animator.SetBool("Running", false);
            }
            else
            {
                animator.SetBool("Inspecting", false);
                animator.SetBool("Walking", false);
                animator.SetBool("Running", false);
            }
        }

    }

    void HandleWander()
    {
        navMeshAgent.speed = wanderSpeed;
        
        if (!waiting)
        {
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
    }

    void HandleInvestigate()
    {
        navMeshAgent.SetDestination(transform.position);
    }
    void HandleChase()
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(chaseTarget.position);

        if (!playerInSight) // Stop chasing if player leaves sight
        {
            if (chaseTimer <= 0)
            {
                state = M3AIState.Wander;
                navMeshAgent.SetDestination(wayPoints[currentWaypoint].position);
                chaseTimer = chaseTime;
            }
            else
            {
                chaseTimer -= Time.deltaTime;
            }
        }
        else
        {
            chaseTimer = chaseTime;
        }
    }

    void MoveToNextWaypoint()
    {
        currentWaypoint = (currentWaypoint + 1) % wayPoints.Count;
        navMeshAgent.SetDestination(wayPoints[currentWaypoint].position);
    }


    public void HostileSpotted(Transform player)
    {
        Debug.Log("Hostile spotted");
        chaseTarget = player;
        state = M3AIState.Chase;
    }

    public void StartInvestigate()
    {
        if (state != M3AIState.Chase)
        {
            state = M3AIState.Investigate;
        }
    }

    public void EndInvestigate()
    {
        if (state == M3AIState.Investigate)
        {
            state = M3AIState.Wander;
        }
    }

    public void SetPlayerInSight(bool inSight)
    {
        playerInSight = inSight;
    }
}
public enum M3AIState
{
    Wander,
    Chase,
    Investigate
}
