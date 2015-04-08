using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ToggleRenderComponents : ButtonAction 
{
	public GameObject theObject;
	public bool initialState = false;
	private List<GameObject> theChildren;

	private bool hasText = false;
	private Text[] childsText;
	private bool hasImage = false;
	private Image[] childsImage;
	private bool hasCollider = false;
	private Collider2D[] childsColliders;

	private bool currentState;

	// Use this for initialization
	void Start () 
	{
		theChildren = new List<GameObject>();

		if (theObject.GetComponentsInChildren<Text>() != null)
		{
			hasText = true;

			childsText = theObject.GetComponentsInChildren<Text>();

			for (int i = 0; i < childsText.Length; i++)
			{
				childsText[i].enabled = initialState;
			}
		}

		if (theObject.GetComponentsInChildren<Image>() != null)
		{
			hasImage = true;
			
			childsImage = theObject.GetComponentsInChildren<Image>();
			
			for (int i = 0; i < childsImage.Length; i++)
			{
				childsImage[i].enabled = initialState;
			}
		}

		if (theObject.GetComponentsInChildren<Collider2D>() != null)
		{
			hasCollider = true;
			
			childsColliders = theObject.GetComponentsInChildren<Collider2D>();
			
			for (int i = 0; i < childsColliders.Length; i++)
			{
				childsColliders[i].enabled = initialState;
			}
		}

	}
	
	public override void takeAction ()
	{
		currentState = !currentState;

		if (hasText)
		{			
			for (int i = 0; i < childsText.Length; i++)
			{
				childsText[i].enabled = currentState;
			}
		}
		
		if (hasImage)
		{
			for (int i = 0; i < childsImage.Length; i++)
			{
				childsImage[i].enabled = currentState;
			}
		}
		
		if (hasCollider)
		{
			for (int i = 0; i < childsColliders.Length; i++)
			{
				childsColliders[i].enabled = currentState;
			}
		}
	}
}
