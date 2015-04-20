using UnityEngine;
using System.Collections;

public class FadeInOnly : ButtonAction 
{
	public FadeInFadeOut objectToFadeIn;

	public override void takeAction()
	{
		Debug.Log("Fading");

		objectToFadeIn.TutorialFade();
	}
}
