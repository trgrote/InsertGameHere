using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just increate tanning and burning of NPC if they are in the sun
[RequireComponent(typeof(NPCData))]
public class NPCTanningBehavior : MonoBehaviour 
{
	public float tanningRate = 0.2f;  // per second

	private float currentTimeInSun = 0f;

	public float burnAcceleration;  // burn/dt

	public float decayMultiplier;

	public float burnRate
	{
		get
		{
			return currentTimeInSun * burnAcceleration;
		}
	}

	NPCData data;

	void Start()
	{
		data = GetComponent<NPCData>();
	}

	void Update()
	{
		if (!data._sm.IsInState(NPCData.eState.Tanning)) return;

		if (data.isInSun)
		{
			data.tanLevel += tanningRate * Time.deltaTime;
			data.burnLevel += burnRate * Time.deltaTime;

			CheckForTanOrBurn();

			currentTimeInSun += Time.deltaTime;
		}
		else
		{
			currentTimeInSun = Mathf.Max(currentTimeInSun - Time.deltaTime * decayMultiplier, 0f);
		}
	}

	// Send Events if we've reached our max
    private void CheckForTanOrBurn()
    {
		if (data.burnLevel > NPCData.maxBurnValue)
		{
			data._sm.Fire(NPCData.eTrigger.CatchFire);
		}
		else if (data.tanLevel > NPCData.maxTanValue)
		{
			data._sm.Fire(NPCData.eTrigger.CompleteTan);
		}
    }
}
