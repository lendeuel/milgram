using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


public class TypeWriter : MonoBehaviour 
{
	public float letterPause = 0.2f;

	public GameObject chatWindow;

	public Material[] theCharactersDialogBoxes;

	public bool isChatWindowOpen;

	bool displayAll;

	Text theTextBox;
	string message;
	Collider thisCollider; 

	Queue myQueueMessages;
	Queue myQueueSpeakers;

	// Use this for initialization
	void Start () 
	{
		myQueueMessages = new Queue ();
		myQueueSpeakers = new Queue ();

		thisCollider = this.collider;
		thisCollider.enabled = false;

		chatWindow.SetActive (false);

		displayAll = false;
		isChatWindowOpen = false;

		theTextBox = GetComponent<Text> ();
		theTextBox.text = "";
	}

	public struct TypeMessageInfo
	{
		string messageToType;
		int whoIsTalking;
		AutoResetEvent autoEvent;

		public TypeMessageInfo(string text, int number, AutoResetEvent e)
		{
			messageToType = text;
			whoIsTalking = number;
			autoEvent = e;
		}

		public AutoResetEvent GetAutoEvent()
		{
			return autoEvent;
		}

		public string GetMessage()
		{
			return messageToType;
		}

		public int GetWhosTalking()
		{
			return whoIsTalking;
		}
	}

	public void Dialog(Dictionary<string, int> dict)
	{
		foreach(KeyValuePair<string, int> kvp in dict)
		{
			myQueueMessages.Enqueue(kvp.Key);
			myQueueSpeakers.Enqueue(kvp.Value);
		}

		TypeMessage ((string)myQueueMessages.Dequeue (), (int)myQueueSpeakers.Dequeue ());		
	}

	public void TypeMessage(string messageToType, int whosTalking)
	{
		thisCollider.enabled = true;

		chatWindow.GetComponent<MeshRenderer> ().material = theCharactersDialogBoxes [whosTalking];

		displayAll = false;

		chatWindow.SetActive (true);

		message = messageToType;
		theTextBox.text = "";

		StartCoroutine(TypeText ());
	}

	void OnMouseUp()
	{
		if (displayAll == false)
		{
			displayAll = true;
		}
		else
		{
		    thisCollider.enabled = false;
			isChatWindowOpen = false;
			chatWindow.SetActive(false);
			theTextBox.text = "";

			if (myQueueMessages.Count > 0)
			{
				TypeMessage ((string)myQueueMessages.Dequeue (), (int)myQueueSpeakers.Dequeue ());
			}
		}
	}

	IEnumerator TypeText () 
	{
		isChatWindowOpen = true;

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

		displayAll = true;
	}
}