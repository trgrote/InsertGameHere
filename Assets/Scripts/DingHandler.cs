using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rho;
using System;

public class DingHandler : MonoBehaviour {
	
	[SerializeField] AudioClip dingNoise;
	[SerializeField] AudioSource source;

	void Awake()
	{
		GlobalEventHandler.Register<NPCLeft>(OnNPCLeft);
	}

    private void OnNPCLeft(NPCLeft evt)
    {
        if (evt.onFire == false) {
			source.PlayOneShot(dingNoise);
		}
    }
}
