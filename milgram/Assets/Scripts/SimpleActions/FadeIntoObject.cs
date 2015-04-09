using UnityEngine;
using System.Collections;

public class FadeIntoObject : MonoBehaviour 
{
	public float delayToStart = 5.0f;
	public float delayToFadeOut = 5.0f;

	private GameObject theGameObject;

	public void FocusOn(GameObject theObject)
	{
		theGameObject = theObject;
		StartCoroutine("Wait", delayToStart);

	}

	public void FocusOn()
	{
		theGameObject = this.gameObject;
		StartCoroutine("Wait", delayToStart);
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
			theGameObject.AddComponent<FadeInFadeOut>().FadeIn();

			yield return new WaitForSeconds(delayToFadeOut);

			theGameObject.GetComponent<FadeInFadeOut>().FadeOut();
		}
	}
}
