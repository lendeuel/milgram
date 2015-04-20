using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnOffThese : ButtonAction 
{
	public List<GameObject> objectsToTurnOff;
	
	public override void takeAction()
	{		
		foreach(GameObject g in objectsToTurnOff)
		{
			g.SetActive(false);
		}
	}
}
