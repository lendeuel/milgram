using UnityEngine;
using System.Collections;

public class DropBoxListener : SimpleDragListener
{
	public AudioClip onMouseOver;
	public AudioClip onDrop;
	private AudioSource source;
	private bool hasPlayedEnter = false;
	private DisplaySprite drawerSprite;

	void Start()
	{
		source = gameObject.AddComponent<AudioSource>();

		GameObject.FindObjectOfType<ListenerManager>().Register(this);

		drawerSprite = GameObject.FindGameObjectWithTag("Drawer").GetComponent<DisplaySprite>();
	}

	void OnMouseEnter()
	{
		if (onMouseOver != null && !hasPlayedEnter)
		{
			hasPlayedEnter = true;
			source.clip = onMouseOver;
			source.Play();
		}

		//drawerSprite.Display(2);
	}

	void OnMouseExit()
	{
		hasPlayedEnter = false;

		//drawerSprite.Display(1);
	}

	public override void OnDrop (GameObject drop)
	{
		if (onDrop != null)
		{
			source.clip = onDrop;
			source.Play();
		}

		drop.GetComponent<Drag>().Reset();

		if (drop.GetComponent<ModifyStats>() != null)
			drop.GetComponent<ModifyStats>().modify();

		if (drop.GetComponent<DialogForks>() != null)
			drop.GetComponent<DialogForks>().takeAction();

		if (drop.GetComponent<FadeInFadeOut>() != null)
			drop.GetComponent<FadeInFadeOut>().FadeOut();
	}
}
