using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CallOnEnable : MonoBehaviour 
{
	public bool isNote;

	// Isn't needed for debugging only
	void Start()
	{
		if (isNote)
		{
			this.GetComponentInChildren<Text>().text = GameObject.FindObjectOfType<NotepadManager>().SetToPage(0);
		}
		else
		{
			GameObject.FindObjectOfType<NotepadManager>().SetToHintPage(0);
		}
	}

	void OnEnable()
	{
		if (isNote)
		{
			this.GetComponentInChildren<Text>().text = GameObject.FindObjectOfType<NotepadManager>().SetToPage(0);
		}
		else
		{
			GameObject.FindObjectOfType<NotepadManager>().SetToHintPage(0);
		}
	}
}
