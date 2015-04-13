using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class DialogQueuer : ButtonAction, TextScroller.TextScrollerEndedResponder
{
	public int lettersPerSecond = 15;

	public MonoBehaviour endedResponse;
	public LineAndSpeaker[] lines;
	public GameObject chatWindow;

	TextScroller s;

	void Start()
	{
		chatWindow = GameObject.FindGameObjectWithTag("DialogHandler");
		s = chatWindow.GetComponentInChildren<TextScroller> ();
	}

	public override void takeAction ()
	{
		chatWindow.GetComponent<Image>().enabled = true;
		chatWindow.GetComponent<BoxCollider2D>().enabled = true;
		chatWindow.GetComponentInChildren<Text>().enabled = true;
		s.addStrings(lines);
		s.endedResponse = endedResponse;
		s.lettersPerSecond = lettersPerSecond;

		try{GameObject.FindGameObjectWithTag("NotepadHints").SetActive(false);}catch(NullReferenceException e){}
		try{GameObject.FindGameObjectWithTag("NotepadNotes").SetActive(false);}catch(NullReferenceException e){}
		try{GameObject.FindGameObjectWithTag("File").SetActive(false);}catch(NullReferenceException e){}
		GameObject.FindGameObjectWithTag("Map").GetComponent<MoveToTools>().MoveToOriginalLocation();
		GameObject.FindGameObjectWithTag("ToolRack").GetComponent<MoveToTools>().MoveToOriginalLocation();

	}

	public void textScrollerEnded()
	{
		Debug.Log("In DialogQueuer textscrollerended");
		chatWindow.GetComponent<Image>().enabled = false;
		chatWindow.GetComponent<BoxCollider2D>().enabled = false;
		chatWindow.GetComponentInChildren<Text>().enabled = false;
	
		if(endedResponse!=null)
		{
			if(endedResponse is TextScroller.TextScrollerEndedResponder)
			{
				(endedResponse as TextScroller.TextScrollerEndedResponder).textScrollerEnded();
			}
			else
			{
				Debug.Log("Ended responder must implement TextScrollerEndedResponder");
			}
		}

		//Destroy(this);
	}
	
}
