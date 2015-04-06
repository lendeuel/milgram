using UnityEngine;
using System.Collections;

public class FadeIntoLocation : MonoBehaviour 
{
	public float delay = 5.0f;
	private GameObject theLocation;

	public void FocusOn(GameObject location)
	{
		theLocation = location;
		StartCoroutine("Wait", delay);
	}

	IEnumerator Wait(float startDelay)
	{
		theLocation.GetComponentInParent<MoveToTools>().MoveToDestination();

		yield return new WaitForSeconds(startDelay/2);

		theLocation.GetComponent<FadeInFadeOut>().FadeIn();

		yield return new WaitForSeconds(startDelay);

		theLocation.GetComponentInParent<MoveToTools>().MoveToOriginalLocation();

		DataHolder.locationsFound++;
	}
}
