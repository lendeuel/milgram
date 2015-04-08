using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CallOnEnable : MonoBehaviour 
{
	void OnEnable()
	{
		this.GetComponentInChildren<Text>().text = GameObject.FindObjectOfType<NotepadManager>().SetToPage(0);
	}
}
