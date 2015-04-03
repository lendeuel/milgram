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
		chatWindow.GetComponent<Image>().enabled = true;
		chatWindow.GetComponent<BoxCollider2D>().enabled = true;
		chatWindow.GetComponentInChildren<Text>().enabled = true;

		s.addStrings(lines);
		s.endedResponse = this;
		s.characterToMaterialMapping = characterToMaterialMapping;
		s.lettersPerSecond = lettersPerSecond;

	}

	public void textScrollerEnded()
	{
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
	}
	
}
