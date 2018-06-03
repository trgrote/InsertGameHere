using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour 
{

	// Use this for initialization
	[SerializeField] GameObject prefab;

	[SerializeField] float timeBetweenSpawn = 10f;

	public void Spawn()
	{
		Instantiate(prefab);
	}

	void Start()
	{
		StartCoroutine(ConstantSpawn());
	}

	IEnumerator ConstantSpawn()
	{
		while (true)
		{
			Spawn();
			yield return new WaitForSeconds(timeBetweenSpawn);
		}
	}
}
