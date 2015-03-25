using UnityEngine;
using System.Collections;

public class ToggleObject : ButtonAction 
{
	public GameObject objectToToggle;

	public override void takeAction ()
	{
		if(objectToToggle.activeSelf)
		{
			objectToToggle.SetActive (false);
		}
		else
		{
			objectToToggle.SetActive (true);
		}
	}

}
