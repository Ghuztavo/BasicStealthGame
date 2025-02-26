using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

// Monster that always knows where the player is
public class Monster1: MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform playerPos;
    Rigidbody rb;

    public GameObject monster;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (monster != null)
        {
            animator = monster.GetComponent<Animator>();
        }
    }

    void Update()
    {
        navMeshAgent.SetDestination(playerPos.position);

        //animation
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }
}