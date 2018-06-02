using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just increate tanning and burning of NPC if they are in the sun
[RequireComponent(typeof(NPCData))]
public class NPCTanningBehavior : MonoBehaviour 
{
	public float tanningRate = 0.1f;  // per second

	private float currentTimeInSun = 0f;

	public float burnAcceleration;  // burn/dt

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
		if (data.isInSun)
		{
			data.tanLevel += tanningRate * Time.deltaTime;
			data.burnLevel += burnRate * Time.deltaTime;

			currentTimeInSun += Time.deltaTime;
		}
		else
		{
			currentTimeInSun = 0f;
		}
	}
}
