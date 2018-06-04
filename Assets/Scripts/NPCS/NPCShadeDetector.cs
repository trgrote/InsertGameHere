using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShadeDetector : MonoBehaviour {
	private NPCData data;
	void OnTriggerStay(Collider other) {
		if (data != null && other.tag == "Shade") {
			data.isInSun = false;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.tag == "Shade")
			data.isInSun = true;
	}

	// Use this for initialization
	void Start () {
		data = GetComponent<NPCData>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
