using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]
public class NPCAnimation : MonoBehaviour 
{
    Animator anim;
    NavMeshAgent agent;

    void Start ()
    {
        anim = GetComponent<Animator> ();
        agent = GetComponent<NavMeshAgent> ();
    }
    
    void Update ()
    {
		var velocity = agent.velocity;
        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update animation parameters
        anim.SetBool("move", shouldMove);
        anim.SetFloat("speed", velocity.magnitude);
    }
}
