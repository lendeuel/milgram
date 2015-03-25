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
<<<<<<< HEAD
		s.addStrings(lines);
		s.endedResponse = this;
		s.characterToMaterialMapping = characterToMaterialMapping;
		chatWindow.SetActive (true);
=======
		GameObject.FindGameObjectWithTag("DialogHandler").collider.enabled = true;
		GameObject.FindGameObjectWithTag("ChatWindow").renderer.enabled = true;
		
		TextScroller s = GameObject.FindGameObjectWithTag("DialogHandler").AddComponent<TextScroller> ();
		Button b = GameObject.FindGameObjectWithTag("DialogHandler").gameObject.AddComponent<Button> ();
		b.action = s;
		s.linesToLoad = lines;
		s.endedResponse = this;
		s.characterToMaterialMapping = characterToMaterialMapping;
		s.lettersPerSecond = lettersPerSecond;
>>>>>>> 15b4ac7f149c751d0e2a0979161b3d0e7d615a0d
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
