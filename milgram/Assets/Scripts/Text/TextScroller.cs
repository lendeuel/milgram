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
	public StatsToModify stats;
	public bool fadeInOnly;
	public FadeInFadeOut gameObjectToFadeIn;

	//[Range (0,1)] public float volume = 1;
	public bool hasSpecificSound = false;
	public AudioClip specificSound;

	[NonSerialized]public bool isGameOverSequence;
	[NonSerialized]public bool hasUserFork;
	[NonSerialized]public UserFork userFork;
	//public bool hasUserFork;
	//public UserFork userFork;

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
	
	public AudioClip[] hintDiscoveredAudio;
	public AudioClip[] locationDiscoveredAudio;
	public AudioClip[] objectiveDiscoveredAudio;
	
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
	
	private AudioSource voiceAudioSource;
	private AudioSource sfxAudioSource;
	
	private Text option1Text;
	private BoxCollider2D option1Collider;
	private ButtonMultipleActions option1Button;
	private Text option2Text;
	private BoxCollider2D option2Collider;
	private ButtonMultipleActions option2Button;

	private Vector3 newLocationStart;
	private Vector3 newHintStart;
	private Vector3 newObjectiveStart;

	void Start()
	{
		newLocationStart = GameObject.FindGameObjectWithTag("NewLocation").GetComponent<Transform>().position;
		newHintStart = GameObject.FindGameObjectWithTag("NewHint").GetComponent<Transform>().position;
		newObjectiveStart = GameObject.FindGameObjectWithTag("NewObjective").GetComponent<Transform>().position;

		option1Text = GameObject.FindGameObjectWithTag("Option1").GetComponent<Text>();
		option2Text = GameObject.FindGameObjectWithTag("Option2").GetComponent<Text>();
		option1Collider = GameObject.FindGameObjectWithTag("Option1").GetComponent<BoxCollider2D>();
		option2Collider = GameObject.FindGameObjectWithTag("Option2").GetComponent<BoxCollider2D>();
		option1Button = GameObject.FindGameObjectWithTag("Option1").GetComponent<ButtonMultipleActions>();
		option2Button = GameObject.FindGameObjectWithTag("Option2").GetComponent<ButtonMultipleActions>();

		voiceAudioSource = GetComponent<AudioSource>();
		sfxAudioSource = gameObject.AddComponent<AudioSource>();

		voiceAudioSource.volume = DataHolder.sfxVolume;
		sfxAudioSource.volume = DataHolder.sfxVolume;

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
		//transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;
		//transform.gameObject.GetComponent<Text>().enabled = false;

		foreach (BoxCollider2D d in transform.parent.gameObject.GetComponentsInChildren<BoxCollider2D>())
		{
			d.enabled = false;
		}
		
		foreach (Text d in transform.parent.gameObject.GetComponentsInChildren<Text>())
		{
			d.enabled = false;
		}
	}
	
	void Update()
	{
		DataHolder.allowInteractions = false;
		
		if(index>=lines.Count)
		{
			if (lines.Count != 0)
			{
				if (lines[index-1].options.hasUserFork)
				{
					Debug.Log("In User Fork Thinger in Text Scroller");
					ProcessUserFork();
				}

				if (lines[index-1].options.isGameOverSequence)
				{
					//Debug.Log("In Game Over");
					ProcessGameOver();
				}
			}

			if(!hasEnded)
			{
				if(endedResponse!=null)
				{
					//Debug.Log("In textScroller ended responder");
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
				voiceAudioSource.Stop();
				
				GetComponent<Text> ().text = lines[index].line;
			}
			else
			{
				if (!voiceAudioSource.isPlaying)
				{
					foreach(CharacterToMaterial c in characterToMaterialMapping)
					{
						if (c.character == lines[index].speaker)
						{
							if (lines[index].options.hasSpecificSound)
							{
								voiceAudioSource.clip = lines[index].options.specificSound;
							}
							else
							{
								int randomClip = UnityEngine.Random.Range(0, c.thisCharactersAudio.Length);
								voiceAudioSource.clip = c.thisCharactersAudio[randomClip];
							}
							
							//source.volume = lines[index].options.volume;
							voiceAudioSource.Play();
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

	public void ProcessGameOver()
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<LoadScene>().Load("MainMenu");
	}

	public void ProcessUserFork() 
	{
		// Set speaker
		foreach(CharacterToMaterial c in characterToMaterialMapping)
		{
			if(c.character == lines[index-1].options.userFork.speaker)
			{
				chatWindow.sprite = c.material;
			}
		}

		// Enable dialog Window 
		chatWindow.GetComponent<Image>().enabled = true;

		// Set Option1's text to option1String
		option1Text.text = lines[index-1].options.userFork.textForOption1;
			
		// Set Option1's action to the fork attached to lines[index-1]
		option1Button.actions[0] = lines[index-1].options.userFork.forkForOption1;

		// Set Option2's text to option2String
		option2Text.text = lines[index-1].options.userFork.textForOption2;
		
		// Set Option2's action to the fork attached to lines[index-1]
		option2Button.actions[0] = lines[index-1].options.userFork.forkForOption2;

		// Enable Text and BoxCollider2D
		option1Text.enabled = true;
		option2Text.enabled = true;
		option1Collider.enabled = true;
		option2Collider.enabled = true;
	}

	public void ProcessFadeInOnly()
	{
		FadeInOnly f = GameObject.FindGameObjectWithTag("GameController").GetComponent<FadeInOnly>();
		lines[index].options.gameObjectToFadeIn.enabled = true;
		f.objectToFadeIn = lines[index].options.gameObjectToFadeIn;
		f.takeAction();
	}

	public void ProcessModify()
	{
		ModifyStats m = GameObject.FindGameObjectWithTag("GameController").GetComponent<ModifyStats>();
		m.stats = lines[index].options.stats;
		m.modify();
	}
	
	public void ProcessKey()
	{
		if (hintDiscoveredAudio.Length != 0)
		{
			int randomClip = UnityEngine.Random.Range(0, hintDiscoveredAudio.Length);
			voiceAudioSource.clip = hintDiscoveredAudio[randomClip];
			sfxAudioSource.Play();
		}

		DataHolder.keysFound++;
		
		GameObject.FindGameObjectWithTag("NewHint").
			GetComponent<FadeIntoObject>().FocusOn(); 
	}
	
	public void ProcessObjective()
	{
		if (objectiveDiscoveredAudio.Length != 0)
		{
			int randomClip = UnityEngine.Random.Range(0, objectiveDiscoveredAudio.Length);
			voiceAudioSource.clip = objectiveDiscoveredAudio[randomClip];
			sfxAudioSource.Play();
		}
		GameObject.FindGameObjectWithTag("NewObjective").GetComponent<FadeInFadeOut>().FadeOut(0);
		GameObject.FindGameObjectWithTag("NewObjective").GetComponent<FadeIntoObject>().FocusOn(); 	

		GameObject.FindGameObjectWithTag("Print").GetComponent<FadeInFadeOut>().FadeOut(0);
		GameObject.FindGameObjectWithTag("Print").GetComponent<FadeIntoObject>().FocusOn();

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
		if (locationDiscoveredAudio.Length != 0)
		{
			int randomClip = UnityEngine.Random.Range(0, locationDiscoveredAudio.Length);
			voiceAudioSource.clip = locationDiscoveredAudio[randomClip];
			sfxAudioSource.Play();
		}
		DataHolder.locationsFound++;

		GameObject.FindGameObjectWithTag("NewLocation").GetComponent<FadeInFadeOut>().FadeOut(0);
		GameObject.FindGameObjectWithTag("NewLocation").GetComponent<FadeIntoObject>().FocusOn(); 

		GameObject.FindGameObjectWithTag("StaticMap").GetComponent<FadeInFadeOut>().FadeOut(0);
		GameObject.FindGameObjectWithTag("StaticMap").GetComponent<FadeIntoObject>().FocusOn();

		Debug.Log(lines[index].line);
		lines[index].options.location.SetActive(true);							
	}
	
	public void ProcessHint()
	{
		if (hintDiscoveredAudio.Length != 0)
		{
			int randomClip = UnityEngine.Random.Range(0, hintDiscoveredAudio.Length);
			voiceAudioSource.clip = hintDiscoveredAudio[randomClip];
			sfxAudioSource.Play();
		}

		DataHolder.hintsFound++;

		GameObject.FindGameObjectWithTag("NewHint").GetComponent<FadeInFadeOut>().FadeOut(0);
		GameObject.FindGameObjectWithTag("NewHint").GetComponent<FadeIntoObject>().FocusOn();

		GameObject.FindGameObjectWithTag("Print").GetComponent<FadeInFadeOut>().FadeOut(0);
		GameObject.FindGameObjectWithTag("Print").GetComponent<FadeIntoObject>().FocusOn(); 

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
			linesToLoad = lines.ToArray();

			// Check to see if next line has a stat requirement.  
			// If it does and it's not met, delete from index+1 to end of lines array
			try 
			{
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
			}
			catch (ArgumentOutOfRangeException e){}

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
				//Debug.Log("Censoring");
				lines[index].line = lines[index].options.censoredText;
			}

			// Reset positions of New items from last call to this method
			GameObject.FindGameObjectWithTag("NewLocation").GetComponent<Transform>().position = newLocationStart;
			GameObject.FindGameObjectWithTag("NewHint").GetComponent<Transform>().position = newHintStart;
			GameObject.FindGameObjectWithTag("NewObjective").GetComponent<Transform>().position = newObjectiveStart;

			int count = 0;
			if (lines[index].options.isHint)
			{
				count++;
			}
			if (lines[index].options.isKey)
			{
				count++;
			}
			if (lines[index].options.isLocation)
			{
				count++;
			}
			if (lines[index].options.isObjective)
			{
				count++;
			}

			int offset = 15;
			if (count > 1)
			{
				if (count == 2)
				{
					if (lines[index].options.isHint || lines[index].options.isKey)
					{
						Vector3 temp = new Vector3(newHintStart.x, newHintStart.y - offset, newHintStart.z);

						GameObject.FindGameObjectWithTag("NewHint").GetComponent<Transform>().position = temp;
					}
					else if (lines[index].options.isLocation)
					{
						Vector3 temp = new Vector3(newLocationStart.x, newLocationStart.y - offset, newLocationStart.z);
						
						GameObject.FindGameObjectWithTag("NewLocation").GetComponent<Transform>().position = temp;
					}
					else if (lines[index].options.isObjective)
					{
						Vector3 temp = new Vector3(newObjectiveStart.x, newObjectiveStart.y - offset, newObjectiveStart.z);
						
						GameObject.FindGameObjectWithTag("NewObjective").GetComponent<Transform>().position = temp;
					}
				}
				else if (count == 3)
				{
					int tempCount = 0;

					if (lines[index].options.isHint)
					{
						tempCount++;
						Vector3 temp = new Vector3(newHintStart.x, newHintStart.y - offset*tempCount, newHintStart.z);
						
						GameObject.FindGameObjectWithTag("NewHint").GetComponent<Transform>().position = temp;
					}

					if (lines[index].options.isLocation)
					{
						tempCount++;
						Vector3 temp = new Vector3(newLocationStart.x, newLocationStart.y - offset*tempCount, newLocationStart.z);
						
						GameObject.FindGameObjectWithTag("NewLocation").GetComponent<Transform>().position = temp;
					}

					if (lines[index].options.isKey && tempCount <= 1)
					{
						tempCount++;
						Vector3 temp = new Vector3(newHintStart.x, newLocationStart.y - offset*tempCount, newLocationStart.z);
						
						GameObject.FindGameObjectWithTag("NewHint").GetComponent<Transform>().position = temp;
					}

					if (lines[index].options.isObjective && tempCount <= 1)
					{
						tempCount++;
						Vector3 temp = new Vector3(newObjectiveStart.x, newObjectiveStart.y - offset*tempCount, newObjectiveStart.z);
						
						GameObject.FindGameObjectWithTag("NewObjective").GetComponent<Transform>().position = temp;
					}

				}
			}
			offset = 0;
			currentTag = 0;
			processing = false;

			if (lines[index].options.fadeInOnly)
			{
				//Debug.Log("Processing Fade In Only.");
				ProcessFadeInOnly();
			}

			if (lines[index].options.isObjective)
			{
				//Debug.Log("Processing Objective.");
				ProcessObjective();
			}
			
			if (lines[index].options.isKey)
			{
				//Debug.Log("Processing Key.");
				ProcessKey();
			}
			if (lines[index].options.isLocation)
			{
				//Debug.Log("Processing Location.");
				ProcessLocation();
			}
			if (lines[index].options.isHint)
			{
				//Debug.Log("Processing Hint.");
				ProcessHint();
			}
			if (lines[index].options.hasModify)
			{
				//Debug.Log("Processing Modify.");
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
