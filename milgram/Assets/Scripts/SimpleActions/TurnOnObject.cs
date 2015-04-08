using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnOnObject : ButtonAction 
{
	public List<GameObject> objectsToTurnOn;

	public override void takeAction ()
	{		
		foreach(GameObject g in objectsToTurnOn)
		{
			g.SetActive (true);
				
			if (g.tag == "File")
			{
				DataHolder.fileOpen = true;
			}
			else if (g.tag == "Notepad")
			{
				DataHolder.notepadOpen = true;
			}

		}
	}
}
