using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour 
{
	private FadeInFadeOut drawerFade;
	private bool fadedIn = false;
	private DisplaySprite drawerSprite;

	public bool oneTimeUse = false;
	private bool interactionsAllowed = true;

	public AudioClip[] onMouseOver;
	public AudioClip[] onClick;

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

		drawerFade = GameObject.FindGameObjectWithTag("DrawerHighlight").GetComponent<FadeInFadeOut>();

		drawerSprite = GameObject.FindGameObjectWithTag("Drawer").GetComponent<DisplaySprite>();
	}

	void OnMouseEnter()
	{
		if (DataHolder.allowInteractions)// && interactionsAllowed)
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

	void OnMouseDown()
	{
		//originalLocation = this.transform.position;

		if (DataHolder.allowInteractions)// && interactionsAllowed)
		{
			if (!fadedIn)
			{
				fadedIn = true;
				drawerFade.FadeIn();
			}

			drawerSprite.Display(1);

			if (onClick.Length != 0 && !hasPlayedClick)
			{
				hasPlayedClick = true;
				int randomClip = UnityEngine.Random.Range(0, onClick.Length);
				source.clip = onClick[randomClip];
				source.Play();
			}

			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}

	void OnMouseDrag()
	{
		if (DataHolder.allowInteractions)// && interactionsAllowed)
		{
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.position = curPosition;
		}
	}

	public void Reset()
	{
		if (oneTimeUse)
		{
			interactionsAllowed = false;

			gameObject.SetActive(false);
			//Destroy(this.gameObject);
		}

		this.transform.localPosition = originalLocation;
	}

	void OnMouseUp()
	{
		hasPlayedClick = false;

		if (DataHolder.allowInteractions)// && interactionsAllowed)
		{
			if (fadedIn)
			{
				fadedIn = false;
				drawerFade.FadeOut();
			}

			drawerSprite.Display(0);

			GameObject.FindObjectOfType<ListenerManager>().OnDrop(gameObject);
			this.transform.localPosition = originalLocation;
		}
	}
}
