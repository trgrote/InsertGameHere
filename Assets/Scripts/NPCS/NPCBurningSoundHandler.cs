using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Match Burning Volume with current burn Rate
public class NPCBurningSoundHandler : MonoBehaviour 
{
	[SerializeField] AudioSource source;

	[SerializeField] AudioClip onFireClip;

	NPCTanningBehavior tanning;

	NPCData data;

	[SerializeField] float maxBurnRate;

	[SerializeField] AnimationCurve volumeCurve;

	void Start()
	{
		tanning = transform.parent.GetComponent<NPCTanningBehavior>();
		data = transform.parent.GetComponent<NPCData>();
	}

	public void OnStateChange()
	{
		if (data != null)
		{
			if (data._sm.IsInState(NPCData.eState.OnFire))
			{
				source.clip = onFireClip;
				source.volume = 1.0f;
				source.Play();
			}
			else if(data._sm.IsInState(NPCData.eState.Tanned))
			{
				source.volume = 0.0f;
			}
		}
	}

	void Update()
	{
		// 0 -> 1
		source.volume = volumeCurve.Evaluate(tanning.burnRate / maxBurnRate);
	}
}
