using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour 
{
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

	void OnMouseDown()
	{
		originalLocation = this.transform.position;

		if (DataHolder.allowInteractions)
		{
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

	void OnMouseUp()
	{
		hasPlayedClick = false;

		if (DataHolder.allowInteractions)
		{
			GameObject.FindObjectOfType<ListenerManager>().OnDrop(gameObject);
			this.transform.position = originalLocation;
		}
	}
}
