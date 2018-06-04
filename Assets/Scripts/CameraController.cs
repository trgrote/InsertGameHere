using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple Camera Controller that just moves the camera left or right
public class CameraController : MonoBehaviour 
{
	[SerializeField] float cameraSpeed = 2f;
	// [SerializeField] float cameraLimitLeft = -50f;
	// [SerializeField] float cameraLimitRight = 50f;

	void Update()
	{
		var horiz = Input.GetAxis("Horizontal");
		if (Mathf.Abs(horiz) > 0)
		{
			transform.position += transform.right * horiz * cameraSpeed;
		}
	}
}
