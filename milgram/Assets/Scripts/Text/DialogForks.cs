using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[Serializable]
public class StatRequirement
{
	public StatSystem.Stats stat;
	public float value;
}

[Serializable]
public class TheseDialogQueuers
{
	private DialogQueuer thisDialogQueuer;
	
	public bool containsKey = false;
	public bool containsLocation = false;
	public bool hasStatRequirement = false;
	public StatRequirement statRequirement;
	[NonSerialized]public bool canBeQueued = true;
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

public class DialogForks : ButtonAction, TextScroller.TextScrollerEndedResponder
{
	public List<TheseDialogQueuers> dialogQueuers;
	
	private List<TheseDialogQueuers> emptyDialogueQueuers;
	
	public DialogForks baseFork;
	private bool hasParent = false;
	
	private MonoBehaviour endedResponseWithLocation;
	private GameObject triggeredLocation;
	
	private DialogQueuer option;
	
	private List<LineAndSpeaker> lines = new List<LineAndSpeaker>();
	
	private GameObject chatWindow;
	
	private bool processLocation = false;
	
	void Start()
	{
		chatWindow = GameObject.FindGameObjectWithTag("DialogHandler");
		
		GameObject go = GameObject.FindGameObjectWithTag("GameController");
		option = go.GetComponent<DialogQueuer>();
		
		// Populate variables
		foreach(TheseDialogQueuers d in dialogQueuers)
		{
			DialogQueuer temp = this.gameObject.AddComponent<DialogQueuer>();
			temp.chatWindow = GameObject.FindGameObjectWithTag("DialogHandler");
			temp.lines = d.lines;
			temp.lettersPerSecond = d.lettersPerSecond;
			temp.endedResponse = this;
			
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
			bool hasAFork = false;
			bool hasAStatRequirement = false;
			int indexOfStatRequirement = 0;
			int indexOfFork = 0;
			
			int count = 0;
			foreach(TheseDialogQueuers t in dialogQueuers)
			{
				if (t.hasStatRequirement)
				{
					// Check to see if the requirement is met
					if (GameObject.FindObjectOfType<StatSystem>().GetValueForStat(t.statRequirement.stat) >= t.statRequirement.value)
					{
						// Requirement was met queue the object then remove from this dialogFork
						hasAStatRequirement = true;
						indexOfStatRequirement = count;
					}
					else
					{
						// If it isn't met it can't be queued.
						t.canBeQueued = false;
					}
				}
				
				count++;
			}
			
			// Before next step make sure there's at least one dialogQueuer that can be queued
			count = 0;
			foreach(TheseDialogQueuers t in dialogQueuers)
			{
				if (t.canBeQueued)
				{
					count++;
				}
			}
			
			Debug.Log(count);
			
			if (count == 0)
			{
				NoneLeft();
				return;
			}
			
			
			int randomNum = 0;
			
			if (hasAStatRequirement)
			{
				randomNum = indexOfStatRequirement;
			}
			else
			{
				randomNum = UnityEngine.Random.Range(0,dialogQueuers.Count);
				
				if (!dialogQueuers[randomNum].canBeQueued)
				{
					bool noneLeft = true;
					
					for(int i = 0; i < dialogQueuers.Count; i++)
					{
						if (dialogQueuers[i].canBeQueued)
						{
							randomNum = i;
							noneLeft = false;
						}
					}
					
					if (noneLeft)
					{
						NoneLeft();
						return;
					}
				}
			}
			
			// If we found a key in this dialog queuer all other branches on this tree are irrelevant
			if (dialogQueuers[randomNum].containsKey || dialogQueuers[randomNum].containsLocation)
			{
				//Debug.Log("Key or location found on " + this.gameObject.name);
				
				dialogQueuers[randomNum].getThisDialogQueuer().takeAction();
				
				if (!hasParent)
				{
					dialogQueuers.RemoveRange(0,dialogQueuers.Count);
				}
				else 
				{
					baseFork.dialogQueuers.RemoveRange(0, baseFork.dialogQueuers.Count);
				}
				
				return;
			}
			
			count = 0;
			foreach(LineAndSpeaker l in dialogQueuers[randomNum].lines)
			{
				if (l.options.hasFork)
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
				AddQueuers(dialogQueuers[randomNum].lines[indexOfFork].options.dialogFork, randomNum);
				dialogQueuers.RemoveAt(randomNum);
			}
		}
		// If it branches into this code it means the user clicked the box when no DialogQueuers were on this DialogFork
		else
		{
			NoneLeft();
			return;
		}
		
	}
	
	public void NoneLeft()
	{
		Debug.Log("In None Left");
		LineAndSpeaker line = new LineAndSpeaker();
		line.speaker = Characters.Suspect;
		line.options.volume = 0;
		line.line = "...";
		lines = new List<LineAndSpeaker>();
		lines.Add(line);
				
		option.lines = lines.ToArray();
		option.endedResponse = this;
		option.takeAction();
	}
	
	public void textScrollerEnded()
	{	
		Debug.Log("In DialogForks textscrollerended");
		
		chatWindow.GetComponent<Image>().enabled = false;
		chatWindow.GetComponent<BoxCollider2D>().enabled = false;
		chatWindow.GetComponentInChildren<Text>().enabled = false;
		
		if(processLocation)
		{
			Debug.Log("In Second Dialogforks textscrollerended");
			if(endedResponseWithLocation is TextScroller.TextScrollerEndedResponder)
			{
				(endedResponseWithLocation as TextScroller.TextScrollerEndedResponder).textScrollerEnded();
			}
			else
			{
				Debug.Log("Ended responder must implement TextScrollerEndedResponder");
			}
		}
		else
		{
			Debug.Log("In Third dialogForks textscrollerended");
		}
		
		//Destroy(this);
	}
	
	public void DestroyThis()
	{
		Destroy (this);
	}
}


