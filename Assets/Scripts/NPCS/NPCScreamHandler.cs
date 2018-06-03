using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rho;
using System;

// Handle the NPC screaming sounds
public class NPCScreamHandler : MonoBehaviour 
{
	[SerializeField] float timeBetweenScreams = 3f;
	[SerializeField] AudioClip[] screamClips;
	[SerializeField] AudioSource source;

	NPCData data;

	void Awake()
	{
		data = GetComponent<NPCData>();
	}

    public void OnNPCStateChange()
    {
        if (data._sm.IsInState(NPCData.eState.OnFire))
		{
			// Hell yeah, start scream time mother fucker
			StartCoroutine(ScreamForever());
		}
    }

	IEnumerator ScreamForever()
	{
		while (true)
		{
			source.PlayOneShot(screamClips[UnityEngine.Random.Range(0, screamClips.Length)]);
			yield return new WaitForSeconds(timeBetweenScreams);
		}
	}
}
