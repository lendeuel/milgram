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

			if (objectToToggle.tag == "File")
			{
				DataHolder.fileOpen = false;
			}
		}
		else
		{
			objectToToggle.SetActive (true);

			if (objectToToggle.tag == "File")
			{
				DataHolder.fileOpen = true;
			}
		}
	}

}
