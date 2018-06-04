using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatSpawner : MonoBehaviour {

	// Use this for initialization
	[SerializeField] GameObject[] treatPrefabs;

	[SerializeField] float minBetweenSpawn = 10f;
	[SerializeField] float maxTimeBetweenSpawn = 15f;
	// [SerializeField] float spawnSquareRadius = 10f;
	// [SerializeField] float spawnHeight = 3f;
	// [SerializeField] int maxTreats = 5;

	GameObject currentTreat;

	public void Spawn()
	{		
		currentTreat = Instantiate(treatPrefabs[UnityEngine.Random.Range(0, treatPrefabs.Length)],
			transform.position,
			Quaternion.identity);
	}

	IEnumerator Start()
	{
		// Stagger the spawn start so not all of them start at once
		yield return new WaitForSeconds(Random.Range(0f, 10f));
		while (true)
		{
			Spawn();
			yield return new WaitForSeconds(Random.Range(minBetweenSpawn, maxTimeBetweenSpawn));
			Destroy(currentTreat);
			yield return new WaitForSeconds(Random.Range(minBetweenSpawn, maxTimeBetweenSpawn));
		}
	}
}
