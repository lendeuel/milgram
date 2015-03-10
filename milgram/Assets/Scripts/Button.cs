using UnityEngine;
using System.Collections;


public class Button : MonoBehaviour 
{
	public ButtonAction action;

	void OnMouseUp()
	{
		Debug.Log ("button clicked");
		action.takeAction();
	}
}
