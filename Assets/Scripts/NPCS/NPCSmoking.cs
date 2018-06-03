using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSmoking : MonoBehaviour {

	[SerializeField] ParticleSystem smoke;
	private NPCData data;

	// Use this for initialization
	void Start () {
		data = transform.parent.GetComponent<NPCData>();
	}
	
	// Update is called once per frame
	void Update () {
		if (smoke.isPlaying) {
			if (data.burnLevel < 80f) {
				smoke.Pause();
			}
		}
	}
}
