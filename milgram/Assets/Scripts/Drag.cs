using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour 
{
	Vector3 screenPoint;
	Vector3 offset;

	TypeWriter tw;

	void Start()
	{
		tw = FindObjectOfType<TypeWriter> ();
	}

	void OnMouseDown()
	{
		if (tw.isChatWindowOpen == false)
		{
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}

	void OnMouseDrag()
	{
		if (tw.isChatWindowOpen == false)
		{
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.position = curPosition;
		}
	}

	void OnMouseUp()
	{
		if (tw.isChatWindowOpen == false)
		{
			GameObject.FindObjectOfType<ListenerManager>().OnDrop(gameObject);
		}
	}
}
