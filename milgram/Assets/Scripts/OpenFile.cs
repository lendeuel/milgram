using UnityEngine;
using System.Collections;

public class OpenFile : MonoBehaviour 
{
	public GameObject fileToOpen;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseUp()
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
