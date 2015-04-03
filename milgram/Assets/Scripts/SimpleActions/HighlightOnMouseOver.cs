using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighlightOnMouseOver : MonoBehaviour 
{
	private Color originalColor;
	public Color highlightColor;  // = Color.yellow;  With color, it auto populates it with a color.  Default "clear"

	private Image thisImage;

	void Start () 
	{
		thisImage = GetComponent<Image>();
		originalColor = thisImage.color;

		// Default Highlight Color
		if (highlightColor == Color.black)
		{
			highlightColor = Color.yellow;
		}
	}
	
	void OnMouseEnter()
	{
		if (DataHolder.allowInteractions)
		{
			thisImage.color = highlightColor;
		}
	}

	void OnMouseDown() 
	{
		thisImage.color = originalColor;
	}
	
	void OnMouseUp() 
	{
		if (DataHolder.allowInteractions)
		{
			if (this.name != "RedX") thisImage.color = highlightColor;
		}
	}

	void OnMouseExit()
	{
		thisImage.color = originalColor;
	}

	void OnMouseDrag()
	{
		thisImage.color = originalColor;
	}
}
