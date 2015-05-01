using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[Serializable]
public class UserFork
{
	public Characters speaker;
	public string textForOption1;
	public DialogForks forkForOption1;
	public string textForOption2;
	public DialogForks forkForOption2;
}
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
	public bool hasBranchStatRequirement = false;
	public StatRequirement statRequirement;
	[NonSerialized]public bool canBeQueued = true;
	public MonoBehaviour endedResponse;
	public LineAndSpeaker[] lines;

	public bool closeOnClick = true;

	public bool hasUserFork;
	public UserFork userFork;

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

	public bool isGameOverSequence = false;

	private bool hasParent = false;
	
	private MonoBehaviour endedResponse;

	private GameObject triggeredLocation;
	
	private DialogQueuer option;
	
	private List<LineAndSpeaker> lines = new List<LineAndSpeaker>();
	
	private GameObject chatWindow;
	
	private bool processLocation = false;

	[NonSerialized]public bool delete = false;

	void Start()
	{
		chatWindow = GameObject.FindGameObjectWithTag("DialogHandler");
		
		GameObject go = GameObject.FindGameObjectWithTag("GameController");
		option = go.GetComponent<DialogQueuer>();

		// Populate variables
		foreach(TheseDialogQueuers d in dialogQueuers)
		{
			if (d.hasUserFork)
			{
				d.lines[d.lines.Length-1].options.hasUserFork = true;
				d.lines[d.lines.Length-1].options.userFork = d.userFork;
			}

			if (isGameOverSequence)
			{
				d.lines[d.lines.Length-1].options.isGameOverSequence = true;
			}

			DialogQueuer temp = this.gameObject.AddComponent<DialogQueuer>();
			temp.chatWindow = GameObject.FindGameObjectWithTag("DialogHandler");
			temp.lines = d.lines;
			temp.endedResponse = this;
			temp.closeOnClick = d.closeOnClick;
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
				if (t.hasBranchStatRequirement)
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
					try
					{
						baseFork.dialogQueuers.RemoveRange(0, baseFork.dialogQueuers.Count);
					}
					catch (NullReferenceException e)
					{
						Debug.Log("Base Fork null, is this what you want?");
					}
				}

				Check();

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

			Check();
		}
		// If it branches into this code it means the user clicked the box when no DialogQueuers were on this DialogFork
		else
		{
			NoneLeft();
			return;
		}
		
	}

	public void Check()
	{
		// Disable highlighter
		if (dialogQueuers.Count == 0)
		{
			NoneLeft();
		}
	}

	public void NoneLeft()
	{
		try
		{
			gameObject.GetComponent<FadeInFadeOut>().FadeOut(0);
			gameObject.GetComponent<FadeInFadeOut>().disabled = true;
		}
		catch(NullReferenceException e){}

		delete = true;

		//Debug.Log("In None Left");
//		LineAndSpeaker line = new LineAndSpeaker();
//		line.speaker = Characters.Suspect;
//		line.options.volume = 0;
//		line.line = "...";
//		lines = new List<LineAndSpeaker>();
//		lines.Add(line);
//				
//		option.lines = lines.ToArray();
//		option.endedResponse = this;
//		option.takeAction();
	}
	
	public void textScrollerEnded()
	{	
		Debug.Log("In DialogForks textscrollerended");
		
		chatWindow.GetComponent<Image>().enabled = false;

		foreach (BoxCollider2D d in chatWindow.GetComponentsInChildren<BoxCollider2D>())
		{
			d.enabled = false;
		}

		foreach (Text d in chatWindow.GetComponentsInChildren<Text>())
		{
			d.enabled = false;
		}

//		if(this is TextScroller.TextScrollerEndedResponder)
//		{
//			(this as TextScroller.TextScrollerEndedResponder).textScrollerEnded();
//		}
//		else
//		{
//			Debug.Log("Ended responder must implement TextScrollerEndedResponder");
//		}
		
		//Destroy(this);
	}
	
	public void DestroyThis()
	{
		Destroy (this);
	}
}


