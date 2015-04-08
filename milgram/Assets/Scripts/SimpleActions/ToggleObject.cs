using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleObject : ButtonAction 
{
	public List<GameObject> objectsToToggle;

	private int length;
	private int activeCount;
	private int inactiveCount;

	void Start ()
	{
		length = objectsToToggle.Count;
	}

	public override void takeAction ()
	{
		foreach(GameObject g in objectsToToggle)
		{
			if(g.activeSelf)
			{
				activeCount++;
			}
			else
			{
				inactiveCount++;
			}
		}

		foreach(GameObject g in objectsToToggle)
		{
			if (activeCount > 0)
			{
				g.SetActive (false);
				
				if (g.tag == "File")
				{
					DataHolder.fileOpen = false;
				}
				else if (g.tag == "Notepad")
				{
					DataHolder.notepadOpen = false;
				}
			}
			else
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

}
