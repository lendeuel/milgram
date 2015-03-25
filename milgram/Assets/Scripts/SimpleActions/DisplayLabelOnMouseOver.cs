using UnityEngine;
using System.Collections;

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
		//CANT GET THIS TO WORK FOR SOME REASON
		thisRenderer.material.color = new Color (1f, 1f, 1f, Mathf.Lerp(0f,1f,55f));
	}
	
	void OnMouseExit()
	{	
		thisRenderer.material.color = new Color (1f, 1f, 1f, Mathf.Lerp(1f,0f,55f));
	}
}
