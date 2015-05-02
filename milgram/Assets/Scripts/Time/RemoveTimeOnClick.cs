using UnityEngine;
using System.Collections;

public class RemoveTimeOnClick : MonoBehaviour 
{
	public int minutesToRemove;

	void OnMouseUp()
	{
		Debug.Log("Subtracting " + minutesToRemove + " minutes.");
		GameObject.FindObjectOfType<TimeManager>().SubtractTime(0, 0, minutesToRemove);
	}
}
