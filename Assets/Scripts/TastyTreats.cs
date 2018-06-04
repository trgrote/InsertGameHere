using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TastyTreats : MonoBehaviour {

	private float xDirection;
	private float yDirection;
	private float zDirection;

	private Vector3 startingPosition;

	[SerializeField] float rotationSpeedPerSecond = 1f;
	[SerializeField] float bobHeight = 5f;
	[SerializeField] float bobPeriod = 3f;

    float randomTimeOffset = 0f;

	// Use this for initialization
	void Start () {
		xDirection = transform.localEulerAngles.x;
		yDirection = transform.localEulerAngles.y;
		zDirection = transform.localEulerAngles.z;

		startingPosition = transform.localPosition;

        randomTimeOffset = Random.Range(0f, 3f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessBob();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        yDirection += rotationSpeedPerSecond * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(xDirection, yDirection, zDirection);
    }

    private void ProcessBob()
    {
        float cycles = (randomTimeOffset + Time.time) / bobPeriod; //grows forever from zero

        const float tau = Mathf.PI * 2f; // just a constant, ~6.28

        float rawSinWave = Mathf.Sin(tau * cycles); // goes from -1 to + 1
        float movementFactor = rawSinWave / 2f + 0.5f; // halve amplitude, shift up. aka 0 to +1

        Vector3 offset = new Vector3(0, movementFactor * bobHeight, 0);
        transform.localPosition = startingPosition + offset;
    }
}
