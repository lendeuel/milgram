using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnOnObject : ButtonAction 
{
	public List<GameObject> objectsToTurnOn;

	public override void takeAction()
	{		
		foreach(GameObject g in objectsToTurnOn)
		{
			g.SetActive(true);
//			if (g.tag == "File")
//			{
//				if (!DataHolder.fileOpen && !DataHolder.notepadOpen)
//				{
//					g.SetActive (true);
//				}
//			}
//			else
//			{
//				g.SetActive(true);
//			}
		}
	}
}
