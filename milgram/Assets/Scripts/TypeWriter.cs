using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour 
{
	public float letterPause = 0.2f;

	public string textToDisplay;

	bool displayAll;

	Text theTextBox;
	string message;

	// Use this for initialization
	void Start () 
	{
		displayAll = false;

		theTextBox = GetComponent<Text> ();

		message = textToDisplay;
		theTextBox.text = "";
		StartCoroutine(TypeText ());
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			displayAll = true;
		}
	}
	IEnumerator TypeText () 
	{
		foreach (char letter in message.ToCharArray()) 
		{
			if (!displayAll)
			{
				theTextBox.text += letter;
				yield return new WaitForSeconds (letterPause);
			}
			else
			{
				theTextBox.text = message;
			}
		}      
	}
}