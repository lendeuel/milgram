using UnityEngine;
using System.Collections;

public class CloseFile : MonoBehaviour 
{
	TypeWriter tw;
	
	void Start()
	{
		tw = FindObjectOfType<TypeWriter> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseDown()
	{
		if (tw.isChatWindowOpen == false)
		{
			this.transform.parent.gameObject.SetActive (false);
		}
	}
}
