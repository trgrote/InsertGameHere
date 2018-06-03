using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExitTrigger : MonoBehaviour 
{
	[SerializeField, TagSelector] string npcTag;

	void OnTriggerEnter(Collider other)
	{
		// If an npc runs into us and is in a leaving state, then destroy them
		if (other.tag == npcTag)
		{
			var data = other.GetComponent<NPCData>();

			if (data == null) return;

			if (data._sm.IsInState(NPCData.eState.Leaving))
				Destroy(other.gameObject);
		}
	}
}
