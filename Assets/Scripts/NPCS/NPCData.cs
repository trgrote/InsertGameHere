using System.Collections;
using System.Collections.Generic;
using Stateless;
using UnityEngine;
using UnityEngine.Events;

// Just the facts about this NPC
public class NPCData : MonoBehaviour 
{
	// Silly Delegate prototype
	public delegate void OnFieldChanged();

	[HideInInspector]
	public float tanLevel = 0f;

	[HideInInspector]
	public float burnLevel = 0f;

	[HideInInspector]
	public bool isInSun = true;

	public enum eState
	{
		Draggable,
			Idle, // just chilling
			WalkingToInterest,
		BeingDragged,
		OnFire,
		Tanned
	}

	public enum eTrigger
	{
		WalkToInterest,
		ReachedInterest,
		BegunDrag,
		StopDrag
	}

	public eState State = eState.Idle;

	public StateMachine<eState, eTrigger> _sm;

	public bool isBeingDragged
	{
		get
		{
			return _sm.IsInState(eState.BeingDragged);
		}
		set
		{
			if (value)
				_sm.Fire(eTrigger.BegunDrag);
			else
				_sm.Fire(eTrigger.StopDrag);
		}
	}
}
