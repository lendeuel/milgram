﻿using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour 
{
	private FadeInFadeOut drawer;
	private bool fadedIn = false;

	public AudioClip onMouseOver;
	public AudioClip onClick;

	Vector3 screenPoint;
	Vector3 offset;
	Vector3 originalLocation;

	private AudioSource source;
	
	private bool hasPlayedEnter = false;
	private bool hasPlayedClick = false;

	void Start()
	{
		originalLocation = this.transform.localPosition;

		source = gameObject.AddComponent<AudioSource>();

		drawer = GameObject.FindGameObjectWithTag("DrawerHighlight").GetComponent<FadeInFadeOut>();
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

	void OnMouseDown()
	{
		//originalLocation = this.transform.position;

		if (DataHolder.allowInteractions)
		{
			if (!fadedIn)
			{
				fadedIn = true;
				drawer.FadeIn();
			}


			if (onClick != null && !hasPlayedClick)
			{
				hasPlayedClick = true;
				source.clip = onMouseOver;
				source.Play();
			}

			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}

	void OnMouseDrag()
	{
		if (DataHolder.allowInteractions)
		{
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.position = curPosition;
		}
	}

	public void Reset()
	{
		this.transform.localPosition = originalLocation;
	}

	void OnMouseUp()
	{
		hasPlayedClick = false;

		if (DataHolder.allowInteractions)
		{
			if (fadedIn)
			{
				fadedIn = false;
				drawer.FadeOut();
			}

			GameObject.FindObjectOfType<ListenerManager>().OnDrop(gameObject);
			this.transform.localPosition = originalLocation;
		}
	}
}
