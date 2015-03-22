using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	public ButtonAction action;

	TypeWriter tw;

	void Start()
	{
		tw = FindObjectOfType<TypeWriter> ();
	}

	void OnMouseUp()
	{
		if (tw.isChatWindowOpen == false)
		{
			Debug.Log ("button clicked");
			action.takeAction();
		}
	}
}
