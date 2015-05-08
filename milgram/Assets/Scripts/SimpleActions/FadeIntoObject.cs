using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIntoObject : MonoBehaviour 
{
	public float delayToStart = 5.0f;
	public float delayToFadeOut = 5.0f;

	private GameObject theGameObject;

	public void FocusOn(GameObject theObject)
	{
		theGameObject = theObject;
		StartCoroutine("Wait");
	}

	public void FocusOn()
	{
		theGameObject = this.gameObject;
		StartCoroutine("Wait");
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(delayToStart);

		if (theGameObject.GetComponent<FadeInFadeOut>() != null)
		{
			theGameObject.GetComponent<FadeInFadeOut>().FadeIn();

			yield return new WaitForSeconds(delayToFadeOut);

			theGameObject.GetComponent<FadeInFadeOut>().FadeOut();
		}
		else
		{
			FadeInFadeOut fd = theGameObject.AddComponent<FadeInFadeOut>();
			fd.gameObjectsToFade = new Image[1];
			fd.gameObjectsToFade[0] = theGameObject.GetComponent<Image>();
			fd.fadeTime = delayToFadeOut;
			fd.maxAlpha = 1f;
			fd.FadeIn();

			yield return new WaitForSeconds(delayToFadeOut);

			fd.FadeOut();
		}
	}
}
