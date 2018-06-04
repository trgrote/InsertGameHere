using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour 
{

	// Use this for initialization
	[SerializeField] GameObject prefab;

	[SerializeField] float startMinTimeBetweenSpawn = 10f;
	[SerializeField] float startMaxTimeBetweenSpawn = 15f;

	[SerializeField] [Range(0,1)] float spawnTimeDecay = 0.02f;

	
	private float minTimeBetweenSpawn;
	private float maxTimeBetweenSpawn;
	
	public void Spawn()
	{
		Instantiate(prefab, transform.position, Quaternion.identity);
	}

	void Start()
	{
		minTimeBetweenSpawn = startMinTimeBetweenSpawn;
		maxTimeBetweenSpawn = startMaxTimeBetweenSpawn;
		StartCoroutine(ConstantSpawn());
	}

	IEnumerator ConstantSpawn()
	{
		while (true)
		{
			Spawn();
			yield return new WaitForSeconds(Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn));
			minTimeBetweenSpawn = minTimeBetweenSpawn * (1 - spawnTimeDecay);
			maxTimeBetweenSpawn = maxTimeBetweenSpawn * (1 - spawnTimeDecay);
		}
	}
}
