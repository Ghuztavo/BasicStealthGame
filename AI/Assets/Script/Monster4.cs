using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

// monster that chases the player if it hears them
public class Monster4 : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform chaseTarget;

    [SerializeField] private List<Transform> wayPoints;
    private int currentWaypoint = 0;
    [SerializeField] private float stayTime = 2.0f;
    private float stayTimer = 0.0f;
    private bool waiting = false;

    private M4AIState state;
    [SerializeField] private float chaseTime = 5.0f;
    private float chaseTimer = 0.0f;
    [SerializeField] private float wanderSpeed = 3.0f;
    [SerializeField] private float chaseSpeed = 5.0f;
    private bool hearingPlayer = false;

    Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        navMeshAgent.SetDestination(wayPoints[0].position);
        state = M4AIState.Wander;
        stayTimer = stayTime;
        chaseTimer = chaseTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case M4AIState.Wander:
                HandleWander();
                break;
            case M4AIState.Investigate:
                HandleInvestigate();
                break;
            case M4AIState.Chase:
                HandleChase();
                break;
        }

        //animation

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

        if (!hearingPlayer) // Stop chasing if player leaves sight
        {
            if (chaseTimer <= 0)
            {
                state = M4AIState.Wander;
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
        state = M4AIState.Chase;
    }

    public void StartInvestigate()
    {
        if (state != M4AIState.Chase)
        {
            state = M4AIState.Investigate;
        }
    }

    public void EndInvestigate()
    {
        if (state == M4AIState.Investigate)
        {
            state = M4AIState.Wander;
        }
    }

    public void SetHearingPlayer(bool inSight)
    {
        hearingPlayer = inSight;
    }
}
public enum M4AIState
{
    Wander,
    Chase,
    Investigate
}
