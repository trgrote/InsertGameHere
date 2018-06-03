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
		// Leave if we can't be dragged
		if (!data._sm.IsInState(NPCData.eState.Draggable)) return;

		data._sm.Fire(NPCData.eTrigger.BegunDrag);
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset =  transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,screenPoint.z));
	}

	void OnMouseDrag()
	{
		// Leave if we can't be dragged
		if (!data._sm.IsInState(NPCData.eState.BeingDragged)) return;

		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	void OnMouseUp()
	{
		// Leave if we can't be dragged
		if (!data._sm.IsInState(NPCData.eState.BeingDragged)) return;

		data._sm.Fire(NPCData.eTrigger.StopDrag);
	}
}
