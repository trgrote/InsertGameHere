using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCData))]
public class MouseDraggable : MonoBehaviour 
{
	Vector3 screenPoint, offset;

	LayerMask TerrainLayerMask;

	NPCData data;

	void Start()
	{
		data = GetComponent<NPCData>();
		TerrainLayerMask = LayerMask.GetMask("Placeable");
	}

	void OnMouseDown()
	{
		// Leave if we can't be dragged
		if (!data._sm.IsInState(NPCData.eState.Draggable)) return;

		data._sm.Fire(NPCData.eTrigger.BegunDrag);
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		// offset =  transform.position - Camera.main.ScreenToWorldPoint(
		// 	new Vector3(Input.mousePosition.x, Input.mousePosition.y,screenPoint.z));

		
		// Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		// RaycastHit hit;
		// if (Physics.Raycast(ray, out hit))
		// 	if (hit.point != null)
		// 		offset = hit.point;

		// if (Physics.Raycast(ray)) { // if it hits the earth and is valid
	}

	void OnMouseDrag()
	{
		// Leave if we can't be dragged
		if (!data._sm.IsInState(NPCData.eState.BeingDragged)) return;

		// Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		// Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		
		Vector3 curPosition;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		RaycastHit hit;
		// if (Physics.Raycast(ray, out hit))
		if (Physics.Raycast(ray, out hit, 600.0f, TerrainLayerMask.value))
			{
				// print(hit.collider);
				if (hit.point != null) {
					curPosition = hit.point;
					transform.position = curPosition;
				}
			}
	}

	void OnMouseUp()
	{
		// Leave if we can't be dragged
		if (!data._sm.IsInState(NPCData.eState.BeingDragged)) return;

		data._sm.Fire(NPCData.eTrigger.StopDrag);
	}
}
