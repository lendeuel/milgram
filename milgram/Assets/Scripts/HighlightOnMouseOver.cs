using UnityEngine;
using System.Collections;

public class HighlightOnMouseOver : MonoBehaviour 
{
	private Color originalColor;
	public Color highlightColor;

	// Use this for initialization
	void Start () 
	{
		originalColor = GetComponent<Renderer>().material.color;

		// If the user hasn't specified a highlight color, make it yellow
		if (highlightColor == Color.clear) 
		{
			highlightColor = Color.yellow;
		}
	}
	
	void OnMouseEnter()
	{
		GetComponent<Renderer>().material.color = highlightColor;
	}

	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = originalColor;
	}
}
