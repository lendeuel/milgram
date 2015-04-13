// If this or that is active, both will be disabled.  Otherwise if this is inactive, and that is active.  This becomes
// active and that becomes inactive.
using UnityEngine;
using System.Collections;

public class ToggleThisTurnOffThat : ButtonAction 
{
	public GameObject thisGameObject;
	public GameObject thatGameObject;

	private bool thatOn = false;

	public override void takeAction ()
	{
		if (!GameObject.FindGameObjectWithTag("Map").GetComponent<MoveToTools>().movedOut)
		{
			if (thisGameObject.activeSelf)
			{
				thisGameObject.SetActive(false);

				if (thisGameObject.tag == "Notepad")
				{
					DataHolder.notepadOpen = false;
				}

			}
			else if (!thatOn)
			{
				thisGameObject.SetActive(true);

				if (thisGameObject.tag == "Notepad")
				{
					DataHolder.notepadOpen = true;
				}
			}

			thatGameObject.SetActive(false);
		}
	}
}