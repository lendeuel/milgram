using UnityEngine;
using System.Collections;

public class HighlightOnMouseOver : MonoBehaviour 
{
	private Color originalColor;
	public Color highlightColor = Color.yellow;
	
	void Start () 
	{
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
