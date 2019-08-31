﻿using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;

    protected NavMeshAgent agent;

    protected Animator animator;

    protected int currentPoint = 0;

    public float radius = 3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //animator.SetFloat("speed", 1.0f);
        animator.SetBool("walking", true);
        agent.SetDestination(patrolPoints[currentPoint].position);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < radius)
        {
            Next();
        }
    }

    void Next ()
    {
        currentPoint++;

        if (currentPoint > patrolPoints.Length -1)
            currentPoint = 0;

        agent.SetDestination(patrolPoints[currentPoint].position);
    }
}