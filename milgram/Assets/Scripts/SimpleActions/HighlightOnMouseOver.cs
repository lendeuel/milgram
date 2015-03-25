using UnityEngine;
using System.Collections;

public class HighlightOnMouseOver : MonoBehaviour 
{
	private Color originalColor;
	public Color highlightColor;  // = Color.yellow;  With color, it auto populates it with a color.  Default "clear"
	
	void Start () 
	{
		// Default Highlight Color
		if (highlightColor == Color.clear)
		{
			highlightColor = Color.yellow;
		}

		originalColor = GetComponent<Renderer>().material.color;
	}
	
	void OnMouseEnter()
	{
		if (DataHolder.allowInteractions)
		{
			this.renderer.material.color = highlightColor;
		}
	}

	void OnMouseExit()
	{
		this.renderer.material.color = originalColor;
	}
}
