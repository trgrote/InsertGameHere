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

	[SerializeField]
	float walkingSpeed;
	[SerializeField]
	float runningSpeed = 15;

	[Header("Tag References")]
	[SerializeField, TagSelector] string interestTag;
	[SerializeField, TagSelector] string fireExitTag;
	[SerializeField, TagSelector] string tannedExitTag;

	NPCData data;

	NavMeshAgent agent;

	void Start()
	{
		data = GetComponent<NPCData>();
		agent = GetComponent<NavMeshAgent>(); 

		data._sm = new StateMachine<NPCData.eState, NPCData.eTrigger>(
			() => data.State, 
			s => data.State = s);

		data._sm.Configure(NPCData.eState.Tanning)
			.Permit(NPCData.eTrigger.CatchFire, NPCData.eState.OnFire)
			.Permit(NPCData.eTrigger.CompleteTan, NPCData.eState.Tanned);
		{
			data._sm.Configure(NPCData.eState.Draggable)
				.SubstateOf(NPCData.eState.Tanning)
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
				.SubstateOf(NPCData.eState.Tanning)
				.Ignore(NPCData.eTrigger.BegunDrag)
				.OnEntry(StopAgent)
				.Permit(NPCData.eTrigger.StopDrag, NPCData.eState.Idle);
		}

		// Define Leaving State
		data._sm.Configure(NPCData.eState.Leaving)
			.OnEntry(SetAgentRunningSpeed);
		{
			data._sm.Configure(NPCData.eState.OnFire)
				.SubstateOf(NPCData.eState.Leaving)
				.OnEntry(FindNearestFireTarget);

			data._sm.Configure(NPCData.eState.Tanned)
				.SubstateOf(NPCData.eState.Leaving)
				.OnEntry(FindNearestTannedTarget);
		}

		// Fake a reached interest call at the begining to restimulate idle
		data._sm.Fire(NPCData.eTrigger.ReachedInterest);
	}

	void StopAgent()
	{
		agent.isStopped = true;
	}

	void SetAgentRunningSpeed()
	{
		agent.speed = runningSpeed;
	}

	#region Finding Items with Tags

	void FindNearestFireTarget()
	{
		Vector3 dest = new Vector3();
		if (FindRandomObjectWithTag(fireExitTag, out dest))
		{
			agent.destination = dest;
			agent.isStopped = false;
		}
		else
		{
			Debug.LogWarning("Can't find fire exit");
		}
	}

	void FindNearestTannedTarget()
	{
		Vector3 dest = new Vector3();
		if (FindRandomObjectWithTag(tannedExitTag, out dest))
		{
			agent.destination = dest;
			agent.isStopped = false;
		}
		else
		{
			Debug.LogWarning("Can't find tanned exit");
		}
	}

	// Helper function for finding a random item with a tag
	bool FindRandomObjectWithTag(string tag, out Vector3 destination)
	{
		var objs = GameObject.FindGameObjectsWithTag(tag);
		destination = Vector3.zero;

		if (objs.Length > 0)
		{
			var rand = objs[Random.Range(0, objs.Length)];
			destination = rand.transform.position;
			return true;
		}

		return false;
	}

	void FindInterest()
	{
		Vector3 dest = new Vector3();
		if (FindRandomObjectWithTag(interestTag, out dest))
		{
			agent.destination = dest;
			agent.isStopped = false;
		}
		else
		{
			Debug.LogWarning("Can't find interesting thing");
		}
	}

	#endregion

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
