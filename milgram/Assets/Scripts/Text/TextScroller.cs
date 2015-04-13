using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[Serializable]
public class Options
{
	public bool isKey;
	public bool isLocation;
	public string notesText;
	public GameObject location;
	public bool hasFork;
	public DialogForks dialogFork;
	public bool isHint;
	public string hintsText;
	public DialogForks hintsDialogFork;
	[Range (0,1)] public float volume = 1;
	public bool hasSpecificSound = false;
	public AudioClip specificSound;
}

[Serializable]
public class LineAndSpeaker
{
	public string line;
	public Characters speaker;
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

	void Start()
	{
		source = GetComponent<AudioSource>();

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

							source.volume = lines[index].options.volume;
							source.Play();
						}
					}	
				}
			
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

	public void ProcessKey()
	{
		DataHolder.keysFound++;
		GameObject.FindGameObjectWithTag("NewHint").
			GetComponent<FadeIntoObject>().FocusOn(); 

		if (lines[index].options.notesText != "")
		{
			GameObject.FindObjectOfType<NotepadManager>().AddLine(lines[index].options.notesText);
		}
	}

	public void ProcessLocation()
	{
		DataHolder.locationsFound++;

		GameObject.FindGameObjectWithTag("NewLocation").GetComponent<FadeIntoObject>().FocusOn(); 

		GameObject.FindGameObjectWithTag("Map").GetComponent<FadeIntoObject>().FocusOn();

		if (lines[index].options.notesText != "")
		{
			GameObject.FindObjectOfType<NotepadManager>().AddLine(lines[index].options.notesText);
		}

		lines[index].options.location.SetActive(true);							
	}

	public void ProcessHint()
	{
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
	}

	public void addStrings(LineAndSpeaker[] s)
	{
		hasEnded = false;
		//Debug.Log (lines.Count);
		DataHolder.allowInteractions = false;
		foreach(LineAndSpeaker l in s)
		{
			lines.Add (l);
		}
		//Debug.Log (lines.Count);
		foreach(LineAndSpeaker l in lines)
		{
			//Debug.Log(l.line);
		}

		foreach(CharacterToMaterial c in characterToMaterialMapping)
		{
			if(c.character == lines[index].speaker)
			{
				//Debug.Log("changing sprite");
				chatWindow.sprite = c.material;
			}
		}

		if(index>lines.Count)
		{
			mTimeElapsed = 0;
		}
	}

	public override void takeAction()
	{
		if(index<lines.Count)
		{
			if(displayAll)
			{
				mTimeElapsed = 0;
				index++;
				if(index<lines.Count)
				{
					foreach(CharacterToMaterial c in characterToMaterialMapping)
					{
						if(c.character == lines[index].speaker)
						{
							chatWindow.sprite = c.material;
						}
					}

					//Debug.Log("Key: " + lines[index].options.isKey + " Location: " + lines[index].options.isLocation + " Hint: " + lines[index].options.isHint);

					//source.volume = lines[index].options.volume;

					offset = 0;
					currentTag = 0;
					processing = false;

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
				}

				displayAll=false;
			}
			else
			{
				displayAll=true;

				GameObject.FindObjectOfType<TimeManager>().DialogueSkipped();
			}
		}
	}

	void OnMouseUp()
	{
		//takeAction ();
	}
}
