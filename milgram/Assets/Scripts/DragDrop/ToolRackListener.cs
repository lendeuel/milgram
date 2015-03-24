using UnityEngine;
using System.Collections;

public class DrawerToolListener : SimpleDragListener
{	
	void Start()
	{
		GameObject.FindObjectOfType<ListenerManager>().Register(this);
	}

	public override void OnDrop (GameObject drop)
	{

	}
}
