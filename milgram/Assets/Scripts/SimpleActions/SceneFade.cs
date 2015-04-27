using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
	public Sprite imageToFade;

	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

	private bool sceneStarting = true;      // Whether or not the scene is still fading in.

	private bool sceneEnding = false;

	private Image image;

	private int sceneToLoadInt = -1;
	private string sceneToLoadString = "";
	private bool loadByIndex = false;

	void Awake ()
	{ 
		GameObject go = GameObject.FindGameObjectWithTag("Fader");
		go.name = "screenFader";
		// Set the texture so that it is the the size of the screen and covers it.
		image = go.AddComponent<Image>();
		image.sprite = imageToFade;

		RectTransform temp = go.GetComponent<RectTransform>();
		temp.sizeDelta = new Vector2(Screen.width, Screen.height);

		//temp.rect.width = Screen.width;
		//temp.rect.height = Screen.height;
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
		image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);
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