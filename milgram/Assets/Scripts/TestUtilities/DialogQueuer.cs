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
	public CharacterToMaterial[] characterToMaterialMapping;
	public GameObject chatWindow;

	TextScroller s;

	void Start()
	{
		s = chatWindow.GetComponentInChildren<TextScroller> ();
	}

	public override void takeAction ()
	{
		s.addStrings(lines);
		s.endedResponse = this;
		s.characterToMaterialMapping = characterToMaterialMapping;
		s.lettersPerSecond = lettersPerSecond;
		chatWindow.SetActive (true);
	}

	public void textScrollerEnded()
	{
		chatWindow.SetActive (false);
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
	}
	
}
