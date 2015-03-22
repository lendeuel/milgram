using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Suspect1Events : MonoBehaviour 
{
	public int objectNumber;

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

				dict.Add("Hi James, my name is Harvey.", 0);
				dict.Add("Hey Harvey, nice to meet you.", 1);
				dict.Add("Not likewise.  Tell me why you are here.", 0);

				foreach (KeyValuePair<string, int> kvp in dict)
				{
					Debug.Log ("Key:" + kvp.Key + " Value:" + kvp.Value);
				}

				tw.Dialog(dict);
			}

			else if (objectNumber == 2) // DOB
			{
				Debug.Log ("I'm an event for Date of Birth");

				tw.TypeMessage("You're too old to be getting in trouble.", 0);
			}

			else if (objectNumber == 3) // Location
			{
				Debug.Log ("I'm an event for Location");
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
				Debug.Log ("I'm an event for Occupation");
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

				tw.TypeMessage("Odd, I don't remember having blue eyes.  Are you sure I'm your guy?", 1);
			}
		}
	}
}
