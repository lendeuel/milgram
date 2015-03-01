using UnityEngine;
using System.Collections;

public class OpenFile : MonoBehaviour 
{
	public GameObject fileOpen;

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
		if (fileOpen.activeSelf == false)
		{
			fileOpen.SetActive(true);
		}
		else 
		{
			fileOpen.SetActive(false);
		}
	}
}
