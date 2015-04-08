using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdvancePage : ButtonAction 
{
	public bool isNotes = false;
	public bool isHints = false;
	public bool isForward = false;
	public Text textToSet;

	public override void takeAction ()
	{
		if (isNotes)
		{
			textToSet.text = GameObject.FindObjectOfType<NotepadManager>().SetPage(isForward);
		}
	}
}
