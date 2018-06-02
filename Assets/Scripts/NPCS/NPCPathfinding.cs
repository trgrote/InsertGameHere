using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPathfinding : MonoBehaviour 
{
	public Transform goal;
	
	void Start () 
	{
		if (goal)
		{
			UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
			agent.destination = goal.position;
		}
	}
}
