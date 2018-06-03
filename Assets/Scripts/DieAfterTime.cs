using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour 
{

	[SerializeField] float minTimeTillDeath = 7f;
	[SerializeField] float maxTimeTillDeath = 14f;

	// Use this for initialization
	IEnumerator Start () 
	{
		yield return new WaitForSeconds(Random.Range(minTimeTillDeath, maxTimeTillDeath));
		Destroy(gameObject);
	}
}
