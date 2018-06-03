using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rho;
using System;

public class NPCLeft : rho.IGameEvent 
{
	public bool onFire = false;
}

// Just listen for when the NPCS leave
public class NPCLeavingListener : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		GlobalEventHandler.Register<NPCLeft>(OnNPCLeft);
	}

	void OnDisabled()
	{
		GlobalEventHandler.Unregister<NPCLeft>(OnNPCLeft);
	}

    private void OnNPCLeft(NPCLeft evt)
    {
		if (evt.onFire)
		{
			++PlayerScore.NumBurned;
		}
		else
		{
			++PlayerScore.NumTanned;
		}
    }
}
