﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPathfinding : MonoBehaviour 
{
	public Transform goal;
	
	void Update () 
	{
		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position;
	}
}
