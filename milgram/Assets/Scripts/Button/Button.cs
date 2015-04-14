using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	public ButtonAction action;
	public AudioClip onMouseOver;
	public AudioClip onClick;
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
			if (onMouseOver != null && !hasPlayedEnter)
			{
				hasPlayedEnter = true;
				source.clip = onMouseOver;
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
			if (onClick != null)
			{
				source.clip = onClick;
				source.Play();
			}

			action.takeAction();
		}
	}
}
