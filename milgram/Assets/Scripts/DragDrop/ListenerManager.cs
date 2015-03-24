using UnityEngine;
using System.Collections.Generic;

public class ListenerManager : MonoBehaviour 
{
	List<DragListener> listeners;

	// Use this for initialization
	void Awake () 
	{
		listeners = new List<DragListener> ();
	}
	
	public void Register(DragListener listener)
	{
		listeners.Add (listener);
	}

	public void OnDrop(GameObject drop)
	{
		foreach(DragListener l in listeners)
		{
			if(l.contains(drop.transform.position))
			{
				l.OnDrop(drop);
			}
		}
	}
}
