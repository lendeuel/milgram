using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	public ButtonAction action;
	public AudioClip[] onMouseOver;
	public AudioClip[] onClick;
	private AudioSource source;

	private bool hasPlayedEnter = false;

	void Start()
	{
		source = gameObject.AddComponent<AudioSource>();
	}

	void OnMouseEnter()
	{
		if (DataHolder.allowInteractions)
		{
			if (onMouseOver.Length != 0 && !hasPlayedEnter)
			{
				hasPlayedEnter = true;
				int randomClip = UnityEngine.Random.Range(0, onMouseOver.Length);
				source.clip = onMouseOver[randomClip];
				source.Play();
			}
		}
	}

	void OnMouseExit()
	{
		hasPlayedEnter = false;
	}

	void OnMouseUp()
	{
		if(DataHolder.allowInteractions || action is TextScroller || action is LoadScene)
		{
			if (onClick.Length != 0)
			{
				int randomClip = UnityEngine.Random.Range(0, onClick.Length);
				source.clip = onClick[randomClip];
				source.Play();
			}

			action.takeAction();
		}
	}
}
