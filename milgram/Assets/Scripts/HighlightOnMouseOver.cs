using UnityEngine;
using System.Collections;

public class HighlightOnMouseOver : MonoBehaviour 
{
	private Color originalColor;
	public Color highlightColor;

	private TypeWriter typeWriter;

	// Use this for initialization
	void Start () 
	{
		typeWriter = FindObjectOfType<TypeWriter> ();

		originalColor = GetComponent<Renderer>().material.color;

		// If the user hasn't specified a highlight color, make it yellow
		if (highlightColor == Color.clear) 
		{
			highlightColor = Color.yellow;
		}
	}
	
	void OnMouseEnter()
	{
		if (typeWriter.isChatWindowOpen == false)
		{
			this.renderer.material.color = highlightColor;
		}
	}

	void OnMouseExit()
	{
		this.renderer.material.color = originalColor;
	}
}
