using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCData))]
public class MouseDraggable : MonoBehaviour 
{
	Vector3 screenPoint, offset;

	NPCData data;

	void Start()
	{
		data = GetComponent<NPCData>();
	}

	void OnMouseDown()
	{
		data.isBeingDragged = true;
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset =  transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,screenPoint.z));
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	void OnMouseUp()
	{
		data.isBeingDragged = false;
	}
}
