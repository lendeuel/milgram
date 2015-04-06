using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	public ButtonAction action;

	void Start()
	{
	}

	void OnMouseUp()
	{
		if(DataHolder.allowInteractions || action is TextScroller || action is LoadScene)
		{
			action.takeAction();
		}
	}
}
