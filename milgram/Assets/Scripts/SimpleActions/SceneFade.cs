using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

	public float startDelay = 3f;

	private bool sceneStarting = true;      // Whether or not the scene is still fading in.

	private bool sceneEnding = false;

	private Image image;
	private Image image2;

	private int sceneToLoadInt = -1;
	private string sceneToLoadString = "";
	private bool loadByIndex = false;

	private GameObject goChild;
	private GameObject go;

	private bool fadeStarted = false;

	void Awake ()
	{ 
		go = GameObject.FindGameObjectWithTag("Fader");
		goChild = GameObject.FindGameObjectWithTag("FaderChild");

		go.name = "screenFader";

		// Set the texture so that it is the the size of the screen and covers it.
		image = go.GetComponent<Image>();

		image2 = goChild.GetComponent<Image>();

		RectTransform temp = go.GetComponent<RectTransform>();
		temp.sizeDelta = new Vector2(Screen.width, Screen.height);

		//temp = goChild.GetComponent<RectTransform>();
		//temp.sizeDelta = new Vector2(Screen.width, Screen.height);
	}
	
	
	void Update ()
	{
		if(sceneStarting)
		{
			StartScene();
		}

		if (sceneEnding)
		{
			EndScene();
		}
	}
	
	IEnumerator Wait(GameObject theGameObject)
	{
		if (theGameObject.GetComponent<FadeInFadeOut>() != null)
		{
			yield return new WaitForSeconds(startDelay);
			
			theGameObject.GetComponent<FadeInFadeOut>().FadeOut(1);
		}
		else
		{
			FadeInFadeOut fd = theGameObject.AddComponent<FadeInFadeOut>();
			fd.gameObjectsToFade = new Image[1];
			fd.gameObjectsToFade[0] = theGameObject.GetComponent<Image>();
			fd.fadeTime = startDelay;
			fd.maxAlpha = 1f;
			
			yield return new WaitForSeconds(startDelay);
			
			fd.FadeOut(1);
		}
	}

	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		image.enabled = true;

		StartCoroutine("Wait", go);
		//image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * .999f * Time.deltaTime);

		if (image2.sprite != null)
		{
			image2.enabled = true;

			StartCoroutine("Wait", goChild);
			//image2.color = Color.Lerp(image.color, Color.clear, fadeSpeed * .999f * Time.deltaTime);
		}
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear
		if (!fadeStarted)
		{
			FadeToClear();

			fadeStarted = true;

			sceneStarting = false;
		}

//		if (image2.sprite != null)
//		{
//			bool blackFadedOut = false;
//			bool imageFadedOut = false;
//
//			// If the texture is almost clear...
//			if(image.color.a <= 0.05f)
//			{
//				// ... set the colour to clear and disable the GUITexture.
//				image.color = Color.clear;
//				image.enabled = false;
//
//				//sceneStarting = false;
//
//				blackFadedOut = true;
//			}
//			if(image2.color.a <= 0.05f)
//			{
//				// ... set the colour to clear and disable the GUITexture.
//				image2.color = Color.clear;
//				image2.enabled = false;
//
//				imageFadedOut = true;
//			}
//
//			// The scene is no longer starting.
//			if (blackFadedOut && imageFadedOut)
//			{
//				sceneStarting = false;
//			}
//		}
//		else
//		{
//			// If the texture is almost clear...
//			if(image.color.a <= 0.05f)
//			{
//				// ... set the colour to clear and disable the GUITexture.
//				image.color = Color.clear;
//				image.enabled = false;
//				
//				// The scene is no longer starting.
//				sceneStarting = false;
//			}
//		}
	}
	
	public void LoadScene(int index)
	{
		loadByIndex = true;

		sceneToLoadInt = index;

		sceneEnding = true;
	}

	public void LoadScene(string sceneName)
	{
		loadByIndex = false;

		sceneToLoadString = sceneName;

		sceneEnding = true;
		sceneStarting = false;
	}

	public void EndScene ()
	{
		// Make sure the texture is enabled.
		image.enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if(image.color.a >= 0.95f)
		{
			if (loadByIndex)
			{
				Application.LoadLevel(sceneToLoadInt);
			}
			else
			{
				Application.LoadLevel(sceneToLoadString);
			}
		}
	}
}