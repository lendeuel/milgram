using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour 
{
	Vector3 screenPoint;
	Vector3 offset;

	TypeWriter tw;
	DropTool dt;
	OpenFile fo;

	void Start()
	{
		tw = FindObjectOfType<TypeWriter> ();
		dt = FindObjectOfType<DropTool> ();
		fo = FindObjectOfType<OpenFile> ();
	}

	void OnCollisionEnter(Collision col)
	{
		//Debug.Log("Drag. OnCollisionEnter. Tag: " + col.collider.tag);

		if (col.collider.tag == "Drawer")
		{
			dt.collidingWithDrawer = true;
		}
	}

	void OnCollisionExit(Collision col)
	{
		//Debug.Log("Drag. OnCollisionExit. Tag: " + col.collider.tag);

		if (col.collider.tag == "Drawer")
		{
			dt.collidingWithDrawer = false;
		}

	}

	void OnMouseDown()
	{
		dt.isMouseUp = false;
		dt.dropped = false;

		if (tw.isChatWindowOpen == false && !fo.fileToOpen.activeSelf)
		{
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - 
				Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}

	void OnMouseDrag()
	{
		if (tw.isChatWindowOpen == false && !fo.fileToOpen.activeSelf)
		{
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.position = curPosition;
		}
	}

	void OnMouseUp()
	{
		dt.isMouseUp = true;
		dt.dropped = true;


		if (tw.isChatWindowOpen == false && !fo.fileToOpen.activeSelf)
		{
			GameObject.FindObjectOfType<ListenerManager>().OnDrop(gameObject);
		}
	}
}
