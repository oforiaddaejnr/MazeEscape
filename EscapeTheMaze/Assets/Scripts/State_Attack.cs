﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_Attack : IState
{
    EnemyController owner;
    NavMeshAgent agent;
    public float chaseWaitTime = 2.0f;
    public float chaseTimer;

    public State_Attack(EnemyController owner) { this.owner = owner; }

    public void Enter()
    {
        Debug.Log("entering attack state");
        agent = owner.GetComponent<NavMeshAgent>();

        if (owner.seenTarget)
        {
            agent.destination = owner.lastSeenPosition;
            agent.isStopped = false;
        }
    }

    public void Execute()
    {
       // Debug.Log("updating attack state");

        agent.destination = owner.lastSeenPosition;
        agent.isStopped = false;
        if (!agent.pathPending && agent.remainingDistance < 5.0f)
        {
            agent.isStopped = true;
        }

        if (owner.seenTarget != true)
        {
           // Debug.Log("lost sight");

            //search for the player
            chaseTimer += Time.deltaTime;

            //Debug.Log("going to last seen position");

            //go to destination of player was last seen
            if (chaseTimer > chaseWaitTime)
            {
                agent.destination = owner.lastSeenPosition;
                agent.isStopped = false;
                if (owner.seenTarget != true)
                {
                   // Debug.Log("switching from attack to patrol");

                    //Go back to patrolling when you lose sight of player
                    owner.stateMachine.ChangeState(new State_Patrol(owner));
                }
            }

        }
        //fire on the player when spotted
        else if (owner.seenTarget == true)
        {
            owner.ShootAtPlayer();
        }

    }

    public void Exit()
    {
       // Debug.Log("exiting attack state");
        agent.isStopped = true;
    }
}
