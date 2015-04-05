using UnityEngine;
using System.Collections;

public class DropBoxListener : SimpleDragListener
{
	void Start()
	{
		GameObject.FindObjectOfType<ListenerManager>().Register(this);
	}
	
	public override void OnDrop (GameObject drop)
	{
		if (drop.GetComponent<ModifyStats>() != null)
			drop.GetComponent<ModifyStats>().modify();

		if (drop.GetComponent<DialogForks>() != null)
			drop.GetComponent<DialogForks>().takeAction();
	}
}
