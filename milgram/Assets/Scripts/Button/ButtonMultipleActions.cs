using UnityEngine;
using System.Collections;

public class ButtonMultipleActions : MonoBehaviour 
{
	public ButtonAction[] actions;

	public AudioClip[] onMouseOver;
	public AudioClip[] onClick;
	public bool runOnce = false;

	private AudioSource source;

	private bool hasTakenAction = false;

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
		//if (DataHolder.allowInteractions)
		{
			if (!hasTakenAction || !runOnce)
			{
				foreach(ButtonAction b in actions)
				{
					//if(b is TextScroller || b is LoadScene)
					//{
						if (onClick.Length != 0)
						{
							int randomClip = UnityEngine.Random.Range(0, onClick.Length);
							source.clip = onClick[randomClip];
							source.Play();
						}

						b.takeAction();
					//}
				}

				if (runOnce) hasTakenAction = true;
			}
		}
	}
}
