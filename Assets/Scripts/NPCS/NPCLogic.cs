using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All the thinking and desires the NPCS have
[RequireComponent(typeof(NPCData))]
public class NPCLogic : MonoBehaviour 
{
	NPCData data;

	void Start()
	{
		data = GetComponent<NPCData>();
	}
}
