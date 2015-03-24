using UnityEngine;
using System.Collections;

public abstract class SimpleDragListener : MonoBehaviour, DragListener
{
	public bool contains(Vector3 position)
	{
		Vector3 zModified = new Vector3 (position.x, position.y, transform.position.z);
		BoxCollider col = GetComponent<BoxCollider> ();
		if(col.bounds.Contains(zModified))
		{
			return true;
		}
		return false;
	}
	
	public abstract void OnDrop (GameObject drop);
}
