using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[Serializable]
public class TheseDialogQueuers
{
	private DialogQueuer thisDialogQueuer;
	public bool isKey = false;
	public bool isLocation = false;
	public int lettersPerSecond = 15;
	public MonoBehaviour endedResponse;
	public LineAndSpeaker[] lines;
	
	public DialogQueuer getThisDialogQueuer()
	{
		return thisDialogQueuer;
	}

	public void setThisDialogQueuer(DialogQueuer d)
	{
		thisDialogQueuer = d;
	}
}

public class DialogForks : ButtonAction
{
	public List<TheseDialogQueuers> dialogQueuers;

	private List<TheseDialogQueuers> emptyDialogueQueuers;

	public DialogForks baseFork;
	private bool hasParent = false;

	private DialogQueuer option;

	private List<LineAndSpeaker> lines = new List<LineAndSpeaker>();

	void Start()
	{
		GameObject go = GameObject.FindGameObjectWithTag("GameController");
		option = go.GetComponent<DialogQueuer>();

		// Populate variables
		foreach(TheseDialogQueuers d in dialogQueuers)
		{
			DialogQueuer temp = this.gameObject.AddComponent<DialogQueuer>();
			temp.chatWindow = GameObject.FindGameObjectWithTag("DialogHandler");
			temp.lines = d.lines;
			temp.lettersPerSecond = d.lettersPerSecond;
			temp.endedResponse = d.endedResponse;

			d.setThisDialogQueuer(temp);
		}
	}

	public void AddQueuers(DialogForks d, int randomNum)
	{
		d.hasParent = true;

		d.takeAction();

		foreach(TheseDialogQueuers t in d.dialogQueuers)
		{
			// Add lines of this dialogQueuer with index of randomNum to a temp array of LineAndSpeaker
			LineAndSpeaker[] temp1 = dialogQueuers[randomNum].lines;

			// Add lines of t.dialogQueuer to a second temp array of LineAndSpeaker
			LineAndSpeaker[] temp2 = t.lines;

			// create new array of LineAndSpeaker with size of temp1 + temp2
			LineAndSpeaker[] both = new LineAndSpeaker[temp1.Length+temp2.Length];

			// Populate array with lineAndSpeakers from temp1 and temp2
			for (int i = 0; i < temp1.Length; i++)
			{
				both[i] = temp1[i];
			}

			for (int i = 0; i < temp2.Length; i++)
			{
				both[i+temp1.Length] = temp2[i];
			}

			// Set lines of this dialogQueuer to the last array
			t.getThisDialogQueuer().lines = both;

			dialogQueuers.Add(t);
		}
	}
	
	public void Remove(TheseDialogQueuers t)
	{
		dialogQueuers.Remove(t);
	}

	public override void takeAction ()
	{
		if (dialogQueuers.Count != 0)
		{
			// This prints one dialogue each time the item is clicked
			bool dialogQueued = false;
			bool hasAFork = false;
			int indexOfFork = 0;

			while (!dialogQueued)
			{
				int randomNum = UnityEngine.Random.Range(0,dialogQueuers.Count);
					
				// If we found a key in this dialog queuer all other branches on this tree are irrelevant
				if (dialogQueuers[randomNum].isKey || dialogQueuers[randomNum].isLocation)
				{
					if (dialogQueuers[randomNum].isKey) DataHolder.keysFound++;
					if (dialogQueuers[randomNum].isLocation) DataHolder.locationsFound++;

					Debug.Log("Key or location found on " + this.gameObject.name);

					dialogQueuers[randomNum].getThisDialogQueuer().takeAction();

					if (!hasParent)
					{
						dialogQueuers.RemoveRange(0,dialogQueuers.Count);
					}
					else 
					{
						baseFork.dialogQueuers.RemoveRange(0, baseFork.dialogQueuers.Count);
					}

					dialogQueued = true;
					return;
				}

				int count = 0;
				foreach(LineAndSpeaker l in dialogQueuers[randomNum].lines)
				{
					if (l.hasFork)
					{
						indexOfFork = count;
						hasAFork = true;
					}

					count++;
				}

				if (!hasAFork)
				{
					dialogQueuers[randomNum].getThisDialogQueuer().takeAction();
					dialogQueuers.RemoveAt(randomNum);
				}
				if (hasAFork)
				{
					dialogQueuers[randomNum].getThisDialogQueuer().takeAction();
					AddQueuers(dialogQueuers[randomNum].lines[indexOfFork].dialogFork, randomNum);
					dialogQueuers.RemoveAt(randomNum);
				}

				dialogQueued = true;
			}


			// *Updated this script since this was commented out.  May or may not do what it says still.*
			// This prints all the dialogue for the item once it's clicked

//				int temp = dialogQueuers.Count;
//			
//				for (int i = 0; i < temp; i++)
//				{
//					bool dialogQueued = false;
//				
//					while (!dialogQueued)
//					{
//						int randomNum = UnityEngine.Random.Range(0,dialogQueuers.Count);
//					
//						if (dialogQueuers[randomNum].isKey)
//						{
//							DataHolder.keysFound++;
//							Debug.Log("Key element found on " + this.gameObject.name);
//						}

//						dialogQueuers[randomNum].getThisDialogQueuer().takeAction();
//					
//						dialogQueuers.RemoveAt(randomNum);
//					
//						dialogQueued = true;
//					}
//				}

		}
		// If it branches into this code it means the user clicked the box when no DialogQueuers were on this DialogFork
		else
		{
			int randomNum = UnityEngine.Random.Range(0, 2);

			LineAndSpeaker line = new LineAndSpeaker();
			line.speaker = Characters.Suspect;
			lines = new List<LineAndSpeaker>();

			if (randomNum == 1)
			{
				line.line = "...";
				lines.Add(line);

			}
			else 
			{
				line.line = "I have nothing more to say about this.";
				lines.Add(line);
			}

			option.lines = lines.ToArray();
			option.takeAction();
		}

	}

	public void DestroyThis()
	{
		Destroy (this);
	}
}


