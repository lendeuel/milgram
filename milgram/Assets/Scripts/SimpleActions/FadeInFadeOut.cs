/*
	FadeObjectInOut.cs
 	Hayden Scott-Baron (Dock) - http://starfruitgames.com
 	6 Dec 2012 

	Modified by: Trevor Newell
	
	This allows you to easily fade an object and its children. 
	If an object is already partially faded it will continue from there. 
	If you choose a different speed, it will use the new speed. 
 
	NOTE: Requires materials with a shader that allows transparency through color.  
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FadeInFadeOut : MonoBehaviour
{
	// publically editable speed
	[NonSerialized]public bool disabled = false;
	public Image[] gameObjectsToFade;
	public float fadeDelay = 0.0f; 
	public float fadeTime = 0.5f; 
	[Range(0,1)]public float maxAlpha = .5f;
	private bool fadeInOnStart = false; 
	private bool fadeOutOnStart = true;
	private bool logInitialFadeSequence = false; 
	private bool fading;

	private bool needsClicked = false;

	// store colours
	private Color[] colors; 
	
	// allow automatic fading on the start of the scene
	IEnumerator Start ()
	{
		Image[] rendererObjects = gameObjectsToFade;
			
		// store the original colours for all child objects
		for (int i = 0; i < rendererObjects.Length; i++)
		{
			rendererObjects[i].color = new Color(rendererObjects[i].color.r,rendererObjects[i].color.g,rendererObjects[i].color.b,maxAlpha);
		}

		//yield return null; 
		yield return new WaitForSeconds (fadeDelay); 
		
		if (fadeInOnStart)
		{
			logInitialFadeSequence = true; 
			FadeIn (); 
		}
		
		if (fadeOutOnStart)
		{
			FadeOut (0); 
		}
	}
	
	// check the alpha value of most opaque object
	float MaxAlpha()
	{
		float maxAlpha = 0.0f; 
		Image[] rendererObjects = gameObjectsToFade;
		foreach (Image item in rendererObjects)
		{
			maxAlpha = Mathf.Max (maxAlpha, item.color.a); 
		}
		return maxAlpha; 
	}
	
	// fade sequence
	IEnumerator FadeSequence (float fadingOutTime)
	{
		// log fading direction, then precalculate fading speed as a multiplier
		bool fadingOut = (fadingOutTime < 0.0f);
		float fadingOutSpeed = 1.0f / fadingOutTime; 

		// grab all child objects
		Image[] rendererObjects = gameObjectsToFade;
		if (colors == null)
		{
			//create a cache of colors if necessary
			colors = new Color[rendererObjects.Length]; 
			
			// store the original colours for all child objects
			for (int i = 0; i < rendererObjects.Length; i++)
			{
				colors[i] = rendererObjects[i].color; 
			}
		}
		
		// make all objects visible
		for (int i = 0; i < rendererObjects.Length; i++)
		{
			rendererObjects[i].enabled = true;
		}
		
		// get current max alpha
		float alphaValue = MaxAlpha();  

		// This is a special case for objects that are set to fade in on start. 
		// it will treat them as alpha 0, despite them not being so. 
		if (logInitialFadeSequence && !fadingOut)
		{
			alphaValue = 0.0f; 
			logInitialFadeSequence = false; 
		}
		
		// iterate to change alpha value 
		while ( (alphaValue >= 0.0f && fadingOut) || (alphaValue <= 1.0f && !fadingOut)) 
		{
			alphaValue += Time.deltaTime * fadingOutSpeed; 

			//Debug.Log("Alpha: " + alphaValue);

			for (int i = 0; i < rendererObjects.Length; i++)
			{
				Color newColor = (colors != null ? colors[i] : rendererObjects[i].color);
				newColor.a = Mathf.Min ( newColor.a, alphaValue ); 
				newColor.a = Mathf.Clamp (newColor.a, 0.0f, 1.0f); 				
				rendererObjects[i].color = newColor; 
			}
			
			yield return null; 
		}
		
		// turn objects off after fading out
		if (fadingOut)
		{
			for (int i = 0; i < rendererObjects.Length; i++)
			{
				//rendererObjects[i].enabled = false; 
			}
		}
		//Debug.Log ("fade sequence end : " + fadingOut); 
	}
	
	void OnMouseEnter()
	{
		if (DataHolder.allowInteractions)
		{
			FadeIn(fadeTime);
		}
	}
	
	void OnMouseDown() 
	{
		if (DataHolder.allowInteractions)
		{
			FadeOut(0);
		}
	}
	
	void OnMouseUp() 
	{
		if (DataHolder.allowInteractions)
		{
			needsClicked = false;

			FadeIn(fadeTime);
		}
	}
	
	void OnMouseDrag()
	{
		if (DataHolder.allowInteractions)
		{
			needsClicked = false;

			FadeOut(0);
		}
	}
	
	void OnMouseExit()
	{	
		if (DataHolder.allowInteractions)
		{
			FadeOut(fadeTime);
		}
	}

	void OnDisable()
	{
		//FadeOut(0);
	}

	public void FadeIn ()
	{
		FadeIn (fadeTime); 
	}
	
	public void FadeOut ()
	{
		FadeOut (fadeTime); 		
	}
	
	public void FadeIn (float newFadeTime)
	{
		//Debug.Log("Fading In: " + newFadeTime);
		if (!disabled && !needsClicked)
		{
			StopAllCoroutines(); 
			StartCoroutine("FadeSequence", newFadeTime); 
		}
	}
	
	public void FadeOut (float newFadeTime)
	{
		//Debug.Log("Fading Out: " + (-newFadeTime));
		if (!disabled && !needsClicked)
		{
			StopAllCoroutines(); 
			StartCoroutine("FadeSequence", -newFadeTime); 
		}
	}

	public void TutorialFade()
	{
		FadeIn();
		needsClicked = true;
	}
	
	// These are for testing only. 
	//		void Update()
	//		{
	//			if (Input.GetKeyDown (KeyCode.Alpha0) )
	//			{
	//				FadeIn();
	//			}
	//			if (Input.GetKeyDown (KeyCode.Alpha9) )
	//			{
	//				FadeOut(); 
	//			}
	//		}
}