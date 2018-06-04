using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSmoking : MonoBehaviour {

	[SerializeField] ParticleSystem smoke;
	[SerializeField] [Range(0f,100f)] float burnLevelThreshold = 80f;
	[SerializeField] float burnRateThreshold = 5f;
	private NPCData data;
	private NPCTanningBehavior tanData;

	// Use this for initialization
	void Start () {
		data = transform.parent.GetComponent<NPCData>();
		tanData = transform.parent.GetComponent<NPCTanningBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		bool shouldBeSmoking = data.burnLevel >= burnLevelThreshold && tanData.burnRate >= burnRateThreshold && !data._sm.IsInState(NPCData.eState.Tanned);

		if (smoke.isPlaying) {
			if (!shouldBeSmoking) {
				smoke.Stop();
			}
		} else {
			if (shouldBeSmoking) {
				smoke.Play();
			}
		}
	}
}
