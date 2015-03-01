using UnityEngine;
using System.Collections;

public class HighlightOnMouseOver : MonoBehaviour 
{
	private Color originalColor;
	public Color highlightColor;

	// Use this for initialization
	void Start () 
	{
		originalColor = renderer.material.color;

		// If the user hasn't specified a highlight color, make it yellow
		if (highlightColor == Color.clear) 
		{
			highlightColor = Color.yellow;
		}
	}
	
	void OnMouseEnter()
	{
		renderer.material.color = highlightColor;
	}

	void OnMouseExit()
	{
		renderer.material.color = originalColor;
	}
}
