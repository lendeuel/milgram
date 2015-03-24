using UnityEngine;
using System.Collections;

public interface DragListener 
{
	bool contains(Vector3 location);
	void OnDrop(GameObject drop);
}
