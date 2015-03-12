using UnityEngine;
using System.Collections;

public class Suspect1Events : MonoBehaviour 
{
	public int objectNumber;

	void OnMouseUp()
	{
		if (objectNumber == 1) // Name
		{
			Debug.Log ("I'm an event for Name");
		}

		else if (objectNumber == 2) // DOB
		{
			Debug.Log ("I'm an event for Date of Birth");
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
		}
	}
}
