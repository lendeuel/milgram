using UnityEngine;
using System.Collections;

public class ToggleObject : ButtonAction 
{
	public GameObject fileToOpen;

	public override void takeAction ()
	{
		if(fileToOpen.activeSelf)
		{
			fileToOpen.SetActive (false);
		}
		else
		{
			fileToOpen.SetActive (true);
		}
	}

}
