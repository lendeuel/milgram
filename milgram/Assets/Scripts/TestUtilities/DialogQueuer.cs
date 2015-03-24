using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public class DialogQueuer : ButtonAction, TextScroller.TextScrollerEndedResponder
{
	public MonoBehaviour endedResponse;
	public LineAndSpeaker[] lines;
	public CharacterToMaterial[] characterToMaterialMapping;

	TextScroller s;
	Button b;

	public override void takeAction ()
	{
		TextScroller s = GameObject.FindGameObjectWithTag("DialogHandler").AddComponent<TextScroller> ();
		Button b = GameObject.FindGameObjectWithTag("DialogHandler").gameObject.AddComponent<Button> ();
		b.action = s;
		s.linesToLoad = lines;
		s.endedResponse = this;
		s.characterToMaterialMapping = characterToMaterialMapping;
	}

	public void textScrollerEnded()
	{
		Destroy (b);
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
