using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour 
{

	// Use this for initialization
	[SerializeField] GameObject prefab;

	[SerializeField] float startMinTimeBetweenSpawn = 10f;
	[SerializeField] float startMaxTimeBetweenSpawn = 15f;

	[SerializeField] float absoluteMinTimeBetweenSpawn = 1f;

	[SerializeField] [Range(0,1)] float spawnTimeDecay = 0.02f;

	
	public float minTimeBetweenSpawn;
	public float maxTimeBetweenSpawn;
	
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
			minTimeBetweenSpawn = Mathf.Max(minTimeBetweenSpawn * (1 - spawnTimeDecay), absoluteMinTimeBetweenSpawn);
			maxTimeBetweenSpawn = Mathf.Max(maxTimeBetweenSpawn * (1 - spawnTimeDecay), absoluteMinTimeBetweenSpawn);
		}
	}
}
