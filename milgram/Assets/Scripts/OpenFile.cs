using UnityEngine;
using System.Collections;

public class OpenFile : MonoBehaviour 
{
	public GameObject fileToOpen;

	TypeWriter tw;
	
	void Start()
	{
		tw = FindObjectOfType<TypeWriter> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseUp()
	{
		if (tw.isChatWindowOpen == false)
		{
			if (fileToOpen.activeSelf == false)
			{
				fileToOpen.SetActive(true);
			}
			else 
			{
				fileToOpen.SetActive(false);
			}
		}
	}
}
