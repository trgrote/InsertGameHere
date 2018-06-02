using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just the facts about this NPC
public class NPCData : MonoBehaviour 
{
	[HideInInspector]
	public float tanLevel = 0f;

	[HideInInspector]
	public float burnLevel = 0f;

	[HideInInspector]
	public bool isInSun = true;
}
