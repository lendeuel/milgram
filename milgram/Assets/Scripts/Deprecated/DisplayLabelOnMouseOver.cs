using UnityEngine;
using System.Collections;

//Fades the label from 0 alpha to 1 alpha over time when the mouse hovers over it
//Fades the label from 1 alpha to 0 alpha over time when the mouse is no longer over it or the player has "grabbed" the object

public class DisplayLabelOnMouseOver : MonoBehaviour 
{
	private Renderer thisRenderer;
	
	// Use this for initialization
	void Start () 
	{		
		thisRenderer = transform.GetChild(0).GetComponent<Renderer>();
		thisRenderer.material.color = new Color(1f,1f,1f,0f);
	}
	
	void OnMouseEnter()
	{
		if (DataHolder.allowInteractions)
		{
			thisRenderer.material.color = new Color (1f, 1f, 1f, Mathf.Lerp(0f,1f,55f));
		}
	}

	void OnMouseDown() 
	{
		if (DataHolder.allowInteractions)
		{
			thisRenderer.material.color = new Color (1f, 1f, 1f, Mathf.Lerp(1f,0f,55f));
		}
	}

	void OnMouseUp() 
	{
		if (DataHolder.allowInteractions)
		{
			thisRenderer.material.color = new Color (1f, 1f, 1f, Mathf.Lerp(0f,1f,55f));
		}
	}

	void OnMouseDrag()
	{
		if (DataHolder.allowInteractions)
		{
			thisRenderer.material.color = new Color (1f, 1f, 1f, Mathf.Lerp(1f,0f,55f));
		}
	}
	
	void OnMouseExit()
	{	
		if (DataHolder.allowInteractions)
		{
			thisRenderer.material.color = new Color (1f, 1f, 1f, Mathf.Lerp(1f,0f,55f));
		}

	}
}
