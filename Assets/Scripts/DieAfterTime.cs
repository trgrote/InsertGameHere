using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour 
{

	[SerializeField] float timeTillDeath = 10f;

	// Use this for initialization
	IEnumerator Start () 
	{
		yield return new WaitForSeconds(timeTillDeath);
		Destroy(gameObject);
	}
}
