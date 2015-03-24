using UnityEngine;
using System.Collections;

public class ToolDragNoParent : MonoBehaviour 
{

	void OnMouseDown()
	{
		transform.parent = null;
	}
}
