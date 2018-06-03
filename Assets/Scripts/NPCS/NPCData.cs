using System.Collections;
using System.Collections.Generic;
using Stateless;
using UnityEngine;
using UnityEngine.Events;

// Just the facts about this NPC
public class NPCData : MonoBehaviour 
{
	public const float maxTanValue = 100f;
	// [HideInInspector]
	public float tanLevel = 0f;

	public const float maxBurnValue = 100f;
	// [HideInInspector]
	public float burnLevel = 0f;

	// [HideInInspector]
	public bool isInSun = true;

	public enum eState
	{
		Tanning,
			Draggable,
				Idle, // just chilling
				WalkingToInterest,
			BeingDragged,
		Leaving,
			OnFire,
			Tanned
	}

	public enum eTrigger
	{
		WalkToInterest,
		ReachedInterest,
		BegunDrag,
		StopDrag,
		CatchFire,
		CompleteTan
	}

	// TODO Make this HideInInspector
	public eState State = eState.Idle;

	public StateMachine<eState, eTrigger> _sm;
}
