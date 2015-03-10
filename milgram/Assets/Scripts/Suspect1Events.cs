using UnityEngine;
using System.Collections;

public class Suspect1Events : MonoBehaviour 
{
	public int objectNumber;

	void OnMouseUp()
	{
		if (objectNumber == 1) // Element 1
		{
			Debug.Log ("I'm an event for Element # 1");
		}

		else if (objectNumber == 2) // Element 2
		{
			Debug.Log ("I'm an event for Element # 2");
		}

		else if (objectNumber == 3) // Element 3
		{
			Debug.Log ("I'm an event for Element # 3");
		}

		else if (objectNumber == 4) // Element 4
		{
			Debug.Log ("I'm an event for Element # 4");
		}

		else if (objectNumber == 5) // Name
		{
			Debug.Log ("I'm an event for Name");
		}

		else if (objectNumber == 6) // Date of Birth
		{
			Debug.Log ("I'm an event for Date of Birth");
		}

		else if (objectNumber == 7) // Mugshot
		{
			Debug.Log ("I'm an event for Mugshot");
		}
	}
}
