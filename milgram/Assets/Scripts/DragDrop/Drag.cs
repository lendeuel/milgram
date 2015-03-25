using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour 
{
	Vector3 screenPoint;
	Vector3 offset;
	Vector3 originalLocation;

	void Start()
	{

	}

	void OnMouseDown()
	{
		originalLocation = this.transform.position;

		if(DataHolder.allowInteractions)
		{
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}

	void OnMouseDrag()
	{
		if(DataHolder.allowInteractions)  
		{
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.position = curPosition;
		}
	}

	void OnMouseUp()
	{
		if(DataHolder.allowInteractions)
		{
			GameObject.FindObjectOfType<ListenerManager>().OnDrop(gameObject);
			this.transform.position = originalLocation;
		}
	}
}
