using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighlightOnMouseOver : MonoBehaviour 
{
	private Color originalColor;
	public Color highlightColor;  // = Color.yellow;  With color, it auto populates it with a color.  Default "clear"

	private bool isText = false;
	private bool isImage = false;

	private Image thisImage;
	private Text thisText;

	void Start () 
	{
		if (GetComponent<Text>() != null)
		{
			isText = true;
			thisText = GetComponent<Text>();
			originalColor = thisText.color;
		}
		else if (GetComponent<Image>() != null)
		{
			isImage = true;
			thisImage = GetComponent<Image>();
			originalColor = thisImage.color;
		}
		
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
			if (isText)
			{
				thisText.color = highlightColor;
			}
			else if (isImage)
			{
				thisImage.color = highlightColor;
			}
		}
	}

	void OnMouseDown() 
	{
		if (isText)
		{
			thisText.color = originalColor;
		}
		else if (isImage)
		{
			thisImage.color = originalColor;
		}	
	}
	
	void OnMouseUp() 
	{
		if (DataHolder.allowInteractions)
		{
			if (this.name != "RedX") 
			{
				if (isText)
				{
					thisText.color = highlightColor;
				}
				else if (isImage)
				{
					thisImage.color = highlightColor;
				}
			}
		}
	}

	void OnMouseExit()
	{
		if (isText)
		{
			thisText.color = originalColor;
		}
		else if (isImage)
		{
			thisImage.color = originalColor;
		}	
	}

	void OnMouseDrag()
	{
		if (isText)
		{
			thisText.color = originalColor;
		}
		else if (isImage)
		{
			thisImage.color = originalColor;
		}	
	}
}
