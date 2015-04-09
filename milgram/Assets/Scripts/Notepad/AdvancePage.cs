using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdvancePage : ButtonAction 
{
	public bool isNotes = false;
	public bool isForward = false;
	public Text textToSet;
	public GameObject theHintsCurrentPage;

	public override void takeAction ()
	{
		if (isNotes)
		{
			textToSet.text = GameObject.FindObjectOfType<NotepadManager>().SetNotepage(isForward);
		}
		else
		{
			GameObject.FindObjectOfType<NotepadManager>().SetHintPage(isForward);
		}
	}
}
