using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AIAgent : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform chaseTarget;

    public List<Transform> wayPoints;
    private int currentWaypoint = 0;

    private AIState state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent.SetDestination(wayPoints[0].position);
        state = AIState.Wander;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == AIState.Wander)
        {
            //when I reach the destination, i will move to the next one
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                currentWaypoint = (currentWaypoint + 1) % wayPoints.Count;
                navMeshAgent.SetDestination(wayPoints[currentWaypoint].position);
            }
        }
        else if (state == AIState.Chase)
        {
            navMeshAgent.SetDestination(chaseTarget.position);
            if (Vector3.Distance(transform.position, chaseTarget.position) > 25)
            {
                state = AIState.Wander;
                navMeshAgent.SetDestination(wayPoints[currentWaypoint].position);
            }
        }

    }

    public void HostileSpotted(Transform hostileTarget)
    {
        Debug.Log("Hostile spotted");
        chaseTarget = hostileTarget;
        navMeshAgent.SetDestination(chaseTarget.position);
        state = AIState.Chase;
    }
}
public enum AIState
{
    Wander,
    Chase
}
