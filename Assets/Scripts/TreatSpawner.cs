using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatSpawner : MonoBehaviour {

	// Use this for initialization
	[SerializeField] GameObject[] treatPrefabs;

	[SerializeField] float timeBetweenSpawn = 10f;
	// [SerializeField] float spawnSquareRadius = 10f;
	// [SerializeField] float spawnHeight = 3f;
	// [SerializeField] int maxTreats = 5;

	public void Spawn()
	{
		// Vector3 relativeSpawnLocation = new Vector3(
		// 	(Random.value - 0.5f) * spawnSquareRadius,
		// 	spawnHeight,
		// 	(Random.value - 0.5f) * spawnSquareRadius);
		
		// Instantiate(treatPrefabs[UnityEngine.Random.Range(0, treatPrefabs.Length)],
		// relativeSpawnLocation + transform.position,
		// Quaternion.identity);
		
		Instantiate(treatPrefabs[UnityEngine.Random.Range(0, treatPrefabs.Length)],
		transform.position,
		Quaternion.identity);
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
