using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
	//public Sprite blackImageToFade;
	//public Sprite startImageToFade;

	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

	private bool sceneStarting = true;      // Whether or not the scene is still fading in.

	private bool sceneEnding = false;

	private Image image;
	private Image image2;

	private int sceneToLoadInt = -1;
	private string sceneToLoadString = "";
	private bool loadByIndex = false;

	private GameObject goChild;

	private bool fadeStarted = false;
	void Awake ()
	{ 
		GameObject go = GameObject.FindGameObjectWithTag("Fader");
		goChild = GameObject.FindGameObjectWithTag("FaderChild");

		go.name = "screenFader";

		// Set the texture so that it is the the size of the screen and covers it.
		image = go.GetComponent<Image>();
		//image.sprite = blackImageToFade;

		image2 = goChild.GetComponent<Image>();
		//image.sprite = startImageToFade;

		//image.color = new Color(image.color.r,image.color.g,image.color.b,1);
		//image2.color = new Color(image.color.r,image.color.g,image.color.b,1);

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
	
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		image.enabled = true;


		image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);

		if (image2.sprite != null)
		{
			//image2.enabled = true;

			//image2.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);
		}
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();

		if (!fadeStarted)
		{
			if (image2.sprite != null)
			{
				goChild.GetComponent<FadeIntoObject>().FocusOn();
			}
		}

		if (image2.sprite != null)
		{
			bool blackFadedOut = false;
			bool imageFadedOut = false;

			// If the texture is almost clear...
			if(image.color.a <= 0.05f)
			{
				// ... set the colour to clear and disable the GUITexture.
				image.color = Color.clear;
				image.enabled = false;

				sceneStarting = false;

				blackFadedOut = true;
			}
//			if(image2.color.a <= 0.05f)
//			{
//				// ... set the colour to clear and disable the GUITexture.
//				image2.color = Color.clear;
//				image2.enabled = false;
//
//				imageFadedOut = true;
//			}

			// The scene is no longer starting.
			if (blackFadedOut && imageFadedOut)
			{
				sceneStarting = false;
			}
		}
		else
		{
			// If the texture is almost clear...
			if(image.color.a <= 0.05f)
			{
				// ... set the colour to clear and disable the GUITexture.
				image.color = Color.clear;
				image.enabled = false;
				
				// The scene is no longer starting.
				sceneStarting = false;
			}
		}
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