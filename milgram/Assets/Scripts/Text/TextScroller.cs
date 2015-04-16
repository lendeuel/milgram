using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[Serializable]
public class Options
{
	public bool hasStatRequirement;
	public StatRequirement statRequired;
	public bool needsCensored;
	public string censoredText;
	public bool isKey;
	public bool isLocation;
	public GameObject location;
	public bool isObjective;
	public string objectiveText;
	public bool hasFork;
	public DialogForks dialogFork;
	public bool isHint;
	public string hintsText;
	public DialogForks hintsDialogFork;
	public bool hasModify;
	public List<StatsToModify> stats;
	
	//[Range (0,1)] public float volume = 1;
	public bool hasSpecificSound = false;
	public AudioClip specificSound;
}

[Serializable]
public class LineAndSpeaker
{
	public string line;
	public Characters speaker;
	public int lettersPerSecond = 15;
	public Options options = new Options();	
}

[Serializable]
public class CharacterToMaterial
{
	public Characters character;
	public Sprite material;
	public AudioClip[] thisCharactersAudio;
}

public class TextScroller : ButtonAction
{
	public interface TextScrollerEndedResponder
	{
		void textScrollerEnded();
		//void FocusOnLocation(GameObject location);
	}
	
	public AudioClip[] hintDiscovered;
	public AudioClip[] locationDiscovered;
	public AudioClip[] objectiveDiscovered;
	
	public bool destroyOnComplete=true;
	public LineAndSpeaker[] linesToLoad;
	public float lettersPerSecond=15;
	public MonoBehaviour endedResponse;
	private float mTimeElapsed=0;
	private int index = 0;
	private bool displayAll=false;
	private List<LineAndSpeaker> lines;
	public CharacterToMaterial[] characterToMaterialMapping;
	private Image chatWindow;
	private bool hasEnded=false;
	
	private string[] splitString;
	private int offset = 0;
	private int currentTag = 0;
	private bool processing = false;
	private bool firstTag = true;
	
	private AudioSource source;
	private AudioSource source2;
	
	void Start()
	{
		source = GetComponent<AudioSource>();
		source2 = gameObject.AddComponent<AudioSource>();
		
		lines = new List<LineAndSpeaker>();
		
		foreach(LineAndSpeaker s in linesToLoad)
		{
			//Debug.Log(s.line);
			lines.Add (s);
		}
		
		chatWindow = GetComponentInParent<Image> ();
		
		if(index<lines.Count)
		{
			foreach(CharacterToMaterial c in characterToMaterialMapping)
			{
				if(c.character == lines[index].speaker)
				{
					chatWindow.overrideSprite = c.material;
				}
			}
		}
		
		transform.parent.gameObject.GetComponent<Image>().enabled = false;
		transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;
		transform.gameObject.GetComponent<Text>().enabled = false;
	}
	
	void Update()
	{
		DataHolder.allowInteractions = false;
		
		if(index>=lines.Count)
		{
			if(!hasEnded)
			{
				if(endedResponse!=null)
				{
					Debug.Log("In textScroller ended responder");
					if(endedResponse is TextScrollerEndedResponder)
					{
						(endedResponse as TextScrollerEndedResponder).textScrollerEnded();
					}
					else
					{
						Debug.Log("Ended responder must implement TextScrollerEndedResponder");
					}
				}
				hasEnded=true;
				//Debug.Log("good im here");
				DataHolder.allowInteractions = true;
				if(destroyOnComplete)
				{
					Destroy(this);
				}
			}
			
			DataHolder.allowInteractions = true;
			
			if(destroyOnComplete)
			{
				Destroy(gameObject);
			}
		}
		else
		{
			if(displayAll)
			{
				source.Stop();
				
				GetComponent<Text> ().text = lines[index].line;
			}
			else
			{
				if (!source.isPlaying)
				{
					foreach(CharacterToMaterial c in characterToMaterialMapping)
					{
						if (c.character == lines[index].speaker)
						{
							if (lines[index].options.hasSpecificSound)
							{
								source.clip = lines[index].options.specificSound;
							}
							else
							{
								int randomClip = UnityEngine.Random.Range(0, c.thisCharactersAudio.Length);
								source.clip = c.thisCharactersAudio[randomClip];
							}
							
							//source.volume = lines[index].options.volume;
							source.Play();
						}
					}	
				}
				
				lettersPerSecond = lines[index].lettersPerSecond;
				
				mTimeElapsed += Time.deltaTime;
				int stoppingPoint = (int)(mTimeElapsed * lettersPerSecond) + offset;
				if(stoppingPoint>lines[index].line.Length)
				{
					stoppingPoint=lines[index].line.Length;
					displayAll=true;
				}
				
				string currChar = "";
				string text = lines [index].line.Substring (0, stoppingPoint);
				if (text.Length != 0)
				{
					currChar = lines[index].line.Substring(text.Length - 1, 1);
				}
				
				bool enteredFirst = false;
				
				if (currChar == "<" && firstTag)
				{
					//Debug.Log("IN < FIRST");
					enteredFirst = true;
					
					firstTag = false;
					
					splitString = lines[index].line.Split(new char[]{'<','>'});
					
					// Increment Offset by size of splitString[1+(4*currentTag)] + 2;
					int tempOffset = splitString[1+(4*currentTag)].Length+2; 
					offset += splitString[1+(4*currentTag)].Length + 2;
					
					text = lines[index].line.Substring(0,stoppingPoint + tempOffset);
					
					processing = true;
					
					//Debug.Log("Temp: " + tempOffset + " Offset: " + offset);
				}
				
				if (processing)
				{
					text += "</color>";
					
					//Debug.Log("IN PROCESSING");
				}
				
				if (currChar == "<" && !firstTag && !enteredFirst)
				{
					//Debug.Log("IN < SECOND");
					
					currentTag++;
					
					firstTag = true;
					
					// Increment Offset by standard size of /color>
					int tempOffset = 7;
					offset += tempOffset;
					
					text = lines[index].line.Substring(0,stoppingPoint + tempOffset);
					
					processing = false;
					
					//Debug.Log("Temp: " + tempOffset + " Offset: " + offset);
				}
				
				GetComponent<Text> ().text = text;
				
				//Debug.Log("Stopping Point: " + stoppingPoint + " Current Char: " + currChar + " Processing: " + processing);
				//Debug.Log(text);
			}
		}
	}
	
	public void ProcessModify()
	{
		ModifyStats m = GameObject.FindGameObjectWithTag("GameController").GetComponent<ModifyStats>();
		m.stats = lines[index].options.stats;
		m.modify();
	}
	
	public void ProcessKey()
	{
		int randomClip = UnityEngine.Random.Range(0, hintDiscovered.Length);
		source.clip = hintDiscovered[randomClip];
		source2.Play();
		
		DataHolder.keysFound++;
		
		GameObject.FindGameObjectWithTag("NewHint").
			GetComponent<FadeIntoObject>().FocusOn(); 
	}
	
	public void ProcessObjective()
	{
		int randomClip = UnityEngine.Random.Range(0, objectiveDiscovered.Length);
		source.clip = objectiveDiscovered[randomClip];
		source2.Play();
		
		GameObject.FindGameObjectWithTag("NewObjective").GetComponent<FadeIntoObject>().FocusOn(); 	

		if (lines[index].options.objectiveText != "")
		{
			GameObject.FindObjectOfType<NotepadManager>().AddLine(lines[index].options.objectiveText);
		}
		else
		{
			Debug.Log("You added a blank objective to the notepad.");
		}
	}
	
	public void ProcessLocation()
	{
		int randomClip = UnityEngine.Random.Range(0, locationDiscovered.Length);
		source.clip = locationDiscovered[randomClip];
		source2.Play();
		
		DataHolder.locationsFound++;
		
		GameObject.FindGameObjectWithTag("NewLocation").GetComponent<FadeIntoObject>().FocusOn(); 
		
		GameObject.FindGameObjectWithTag("Map").GetComponent<FadeIntoObject>().FocusOn();

		Debug.Log(lines[index].line);
		lines[index].options.location.SetActive(true);							
	}
	
	public void ProcessHint()
	{
		int randomClip = UnityEngine.Random.Range(0, hintDiscovered.Length);
		source.clip = hintDiscovered[randomClip];
		source2.Play();
		
		DataHolder.hintsFound++;
		
		GameObject.FindGameObjectWithTag("NewHint").
			GetComponent<FadeIntoObject>().FocusOn();
		
		if (lines[index].options.hintsText != "")
		{
			GameObject.FindObjectOfType<NotepadManager>()
				.AddHint(lines[index].options.hintsText,lines[index].options.hintsDialogFork);
		}
		else 
		{
			Debug.Log("Hints text can't be null.");
		}
		
	}
	
	public void addString(LineAndSpeaker s)
	{
		hasEnded = false;
		
		DataHolder.allowInteractions = false;
		
		lines.Add (s);
		
		if(index>=lines.Count)
		{
			mTimeElapsed = 0;
		}
		
		ProcessNew();
	}
	
	public void addStrings(LineAndSpeaker[] s)
	{
		hasEnded = false;
		
		DataHolder.allowInteractions = false;
		
		foreach(LineAndSpeaker l in s)
		{
			lines.Add (l);
		}
		
		foreach(CharacterToMaterial c in characterToMaterialMapping)
		{
			if(c.character == lines[index].speaker)
			{
				chatWindow.sprite = c.material;
			}
		}
		
		if(index>lines.Count)
		{
			mTimeElapsed = 0;
		}
		
		ProcessNew();
	}
	
	public override void takeAction()
	{
		if(index<lines.Count)
		{
			if(displayAll)
			{
				mTimeElapsed = 0;
				index++;
				
				ProcessNew();
			}
			else
			{
				displayAll=true;
				
				GameObject.FindObjectOfType<TimeManager>().DialogueSkipped();
			}
		}
	}
	
	void ProcessNew()
	{
		if(index<lines.Count)
		{
			// Check to see if next line has a stat requirement.  
			// If it does and it's not met, delete from index+1 to end of lines array
			if (lines[index+1].options.hasStatRequirement)
			{
				// Check to see if the requirement is met
				if (GameObject.FindObjectOfType<StatSystem>().GetValueForStat(lines[index+1].options.statRequired.stat) >= lines[index+1].options.statRequired.value)
				{
					// Requirement was met, do nothing
				}
				else
				{
					// Requirement wasn't met, remove all lines past current index.
					lines.RemoveRange(index+1, lines.Count - (index+1));
				}
			}
			
			foreach(CharacterToMaterial c in characterToMaterialMapping)
			{
				if(c.character == lines[index].speaker)
				{
					chatWindow.sprite = c.material;
				}
			}
			
			//Debug.Log("Key: " + lines[index].options.isKey + " Location: " + lines[index].options.isLocation + " Hint: " + lines[index].options.isHint);
			
			if (lines[index].options.needsCensored && DataHolder.censorText)
			{
				Debug.Log("Censoring");
				lines[index].line = lines[index].options.censoredText;
			}
			
			offset = 0;
			currentTag = 0;
			processing = false;
			
			if (lines[index].options.isObjective)
			{
				Debug.Log("Processing Objective.");
				ProcessObjective();
			}
			
			if (lines[index].options.isKey)
			{
				Debug.Log("Processing Key.");
				ProcessKey();
			}
			if (lines[index].options.isLocation)
			{
				Debug.Log("Processing Location.");
				ProcessLocation();
			}
			if (lines[index].options.isHint)
			{
				Debug.Log("Processing Hint.");
				ProcessHint();
			}
			if (lines[index].options.hasModify)
			{
				Debug.Log("Processing Modify.");
				ProcessModify();
			}
		}
		
		displayAll=false;
	}
	
	void OnMouseUp()
	{
		//takeAction ();
	}
}
