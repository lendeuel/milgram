using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Suspect1Events : MonoBehaviour 
{
	public int objectNumber;
	public StatSystem statSystem;

	TypeWriter tw;

	Dictionary<string, int> dict;

	void Start()
	{
		tw = FindObjectOfType<TypeWriter> ();
	}

	void OnMouseUp()
	{
		dict = new Dictionary<string, int> ();

		if (tw.isChatWindowOpen == false)
		{
			if (objectNumber == 1) // Name
			{
				Debug.Log ("I'm an event for Name");

//				dict.Add("Hi James, my name is Harvey.", 0);
//				dict.Add("Hey Harvey, nice to meet you.", 1);
//				dict.Add("Not likewise.  Tell me why you are here.", 0);
//				tw.Dialog(dict);

				// The Above can be done like so.  It should be done this way in case another message is currently being displayed.
				//tw.QueueMessage("Hi James, my name is Harvey.", 0);
				//tw.QueueMessage("Hey Harvey, nice to meet you.", 1);
				//tw.QueueMessage("Not likewise.  Tell me why you are here.", 0);

				tw.QueueMessage("James, is it?", 0); 
				tw.QueueMessage("... ... hrmph. Dickbag, is it?", 1); 
			}

			else if (objectNumber == 2) // DOB
			{
				Debug.Log ("I'm an event for Date of Birth");

				tw.QueueMessage("You're too old to be getting in trouble.", 0);
				tw.QueueMessage("Yeah, and you're too old to be doing police work.",1); 
			}

			else if (objectNumber == 3) // Location
			{
//				if(StatSystem.willpower >= 2) {
//					tw.QueueMessage("Says here that you were picked up with your son at the park with your son...", 0); 
//					int tempNumber = Random.Range(0,3); 
//					switch(tempNumber) {
//					case 0:
//						tw.QueueMessage("Don't bring family into this.",1); 
//						StatSystem.Stats.willpower--;
//						break; 
//					case 1:
//						tw.QueueMessage("That's some prime detective work you got going for you there.",1); 
//						StatSystem.Stats.willpower--;
//						break; 
//					case 2:
//						tw.QueueMessage("Says here that you some how get paid for this.",1); 
//						break; 
//					case 3:
//						tw.QueueMessage("... What did you say?",1); 								
//						StatSystem.Stats.willpower--;
//						break;
//					}
//				}
//
//				if(StatSystem.Stats.willpower <= 0 || StatSystem.Stats.willingness > 10) {
//					tw.QueueMessage("I have kids too, however, your kid...",0);
//					tw.QueueMessage("If you say another goddamn word about my kid, I swear to God--",1); 
//					tw.QueueMessage("--Seems you hit a nerve there, feel free to use 'appropriate interrogation techniques' you think will get this guy singing.", 0);
//					tw.QueueMessage("He's almost there, keep up the good work",0);
//					tw.QueueMessage("Oh, I'll handle all the 'dirty work' so you can keep your HANDS CLEAN"); 
//				}
			}

			else if (objectNumber == 4) // Previous Convictions
			{
				Debug.Log ("I'm an event for Previous Convictions");
			}
			
			else if (objectNumber == 5) // Personal Affects
			{
				Debug.Log ("I'm an event for Personal Affects");
			}

			else if (objectNumber == 6) // Stats
			{
				Debug.Log ("I'm an event for Stats");
			}

			else if (objectNumber == 7) // Occupation
			{
				tw.QueueMessage("So you're a chemist, eh?", 0); 
				tw.QueueMessage("No shit Sherlock. I used to work at Delmont University. Jesus ...", 1); 
			}

			else if (objectNumber == 8) // Known Affiliates
			{
				Debug.Log ("I'm an event for Known Affiliates");
			}

			else if (objectNumber == 9) // Reason For Arrest
			{
				Debug.Log ("I'm an event for Reason For Arrest");
			}

			else if (objectNumber == 10) // Portrait
			{
				Debug.Log ("I'm an event for Portrait");

				tw.QueueMessage("Odd, I don't remember having blue eyes.  Are you sure I'm your guy?", 1);
			}
		}
	}
}
