using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rho;
using System;

public class DingHandler : MonoBehaviour {
	NPCData data;
	
	[SerializeField] AudioClip dingNoise;
	[SerializeField] AudioSource source;

	void Awake()
	{
		data = GetComponent<NPCData>();
		GlobalEventHandler.Register<NPCLeft>(OnNPCLeft);
	}

    private void OnNPCLeft(NPCLeft evt)
    {
        if (evt.onFire == false) {
			source.PlayOneShot(dingNoise);
		}
    }
}
