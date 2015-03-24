using UnityEngine;
using System.Collections;

public class PrintHiIfDropped : SimpleDragListener
{
	void Start()
	{
		GameObject.FindObjectOfType<ListenerManager>().Register(this);
	}

	override public void OnDrop(GameObject drop)
	{
		Debug.Log ("Hi");
	}
}
