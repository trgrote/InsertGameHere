using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;
using UnityEngine.AI;

// All the thinking and desires the NPCS have
// Basically, this logic is to just have the NPC wait around and then decide to go find a new thing
[RequireComponent(typeof(NPCData))]
[RequireComponent(typeof(NavMeshAgent))]
public class NPCLogic : MonoBehaviour 
{
	[SerializeField]
	float minIdleTime = 1f, maxIdleTime = 10f;

	[SerializeField, Tooltip("How close we should be to say we've reached the interest")]
	float interestCloseValue = 10f;

	NPCData data;

	NavMeshAgent agent;

	void Start()
	{
		data = GetComponent<NPCData>();
		agent = GetComponent<NavMeshAgent>(); 

		data._sm = new StateMachine<NPCData.eState, NPCData.eTrigger>(
			() => data.State, 
			s => data.State = s);

		data._sm.Configure(NPCData.eState.Draggable)
			.Permit(NPCData.eTrigger.BegunDrag, NPCData.eState.BeingDragged);
		{
			data._sm.Configure(NPCData.eState.Idle)
				.SubstateOf(NPCData.eState.Draggable)
				.OnEntry(StartCountDownToWalkToInterest)
				.OnExit(StopCountDownToWalkToInterest)
				.Permit(NPCData.eTrigger.WalkToInterest, NPCData.eState.WalkingToInterest)
				.PermitReentry(NPCData.eTrigger.ReachedInterest);

			data._sm.Configure(NPCData.eState.WalkingToInterest)
				.SubstateOf(NPCData.eState.Draggable)
				.OnEntry(FindInterest)
				.OnEntry(StartWaitToReachInterest)
				.OnExit(StopWaitToReachInterest)
				.Permit(NPCData.eTrigger.ReachedInterest, NPCData.eState.Idle)
				.Permit(NPCData.eTrigger.BegunDrag, NPCData.eState.BeingDragged);
		}

		data._sm.Configure(NPCData.eState.BeingDragged)
			.Ignore(NPCData.eTrigger.BegunDrag)
			.OnEntry(StopAgent)
			.Permit(NPCData.eTrigger.StopDrag, NPCData.eState.Idle);

		// Fake a reached interest call at the begining to restimulate idle
		data._sm.Fire(NPCData.eTrigger.ReachedInterest);
	}

	void StopAgent()
	{
		agent.isStopped = true;
	}

	#region Walking to Interest

	IEnumerator WaitToReachInterestCoroutine = null;

	void StartWaitToReachInterest()
	{
		StopWaitToReachInterest();
		StartCoroutine(WaitToReachInterestCoroutine = WaitToReachInterest());
	}

	void StopWaitToReachInterest()
	{
		if (WaitToReachInterestCoroutine != null)
			StopCoroutine(WaitToReachInterestCoroutine);
	}

	IEnumerator WaitToReachInterest()
	{
		while (agent.remainingDistance > interestCloseValue)
		{
			yield return null;
		}
		agent.isStopped = true;
		data._sm.Fire(NPCData.eTrigger.ReachedInterest);
	}

	void FindInterest()
	{
		var objs = GameObject.FindGameObjectsWithTag("Interest");

		if (objs.Length > 0)
		{
			var rand = objs[Random.Range(0, objs.Length)];
			agent.destination = rand.transform.position;
			agent.isStopped = false;
		}		
	}

	#endregion

	#region Wait to do something interesting

	private IEnumerator CountDownCoroutine = null;

	void StartCountDownToWalkToInterest()
	{
		StopCountDownToWalkToInterest();

		StartCoroutine(CountDownCoroutine = CountDownToWalkToInterest());
	}

	void StopCountDownToWalkToInterest()
	{
		if (CountDownCoroutine != null)
		{
			StopCoroutine(CountDownCoroutine);
		}
	}

	IEnumerator CountDownToWalkToInterest()
	{
		yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
		data._sm.Fire(NPCData.eTrigger.WalkToInterest);
		yield return null;
	}

	#endregion
}
