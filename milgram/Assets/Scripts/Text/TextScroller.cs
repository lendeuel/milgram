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
}

[Serializable]
public class LineAndSpeaker
{
	public string line;
	public Characters speaker;
	public Options options;	
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
							int randomClip = UnityEngine.Random.Range(0, c.thisCharactersAudio.Length);
							source.clip = c.thisCharactersAudio[randomClip];
							source.Play();
						}
					}	
				}
			
				mTimeElapsed += Time.deltaTime;
				int stoppingPoint = (int)(mTimeElapsed * lettersPerSecond);
				if(stoppingPoint>lines[index].line.Length)
				{
					stoppingPoint=lines[index].line.Length;
					displayAll=true;
				}
				string text = lines [index].line.Substring (0, stoppingPoint);
				GetComponent<Text> ().text = text;
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

		GameObject.FindGameObjectWithTag("NewLocation").
			GetComponent<FadeIntoObject>().FocusOn(); 

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
		//Debug.Log ("box clicked");
		if(index<lines.Count)
		{
			if(displayAll)
			{
				mTimeElapsed = 0;
				index++;
				if(index<lines.Count)
				{
					//Debug.Log("In 1: " + index);
					foreach(CharacterToMaterial c in characterToMaterialMapping)
					{
						if(c.character == lines[index].speaker)
						{
							//Debug.Log("changing sprite");
							chatWindow.sprite = c.material;
						}
					}

					Debug.Log("Key: " + lines[index].options.isKey + " Location: " + lines[index].options.isLocation + " Hint: " + lines[index].options.isHint);

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

					//					if (dialogQueuers[randomNum].isKey) DataHolder.keysFound++;
					//
					//					if (dialogQueuers[randomNum].isLocation)
					//					{
					//						GameObject.FindGameObjectWithTag("NewLocation").
					//							GetComponent<FadeIntoObject>().FocusOn(); 
					//
					//						dialogQueuers[randomNum].location.SetActive(true);
					//
					////						triggeredLocation = dialogQueuers[randomNum].location;
					////						processLocation = true;
					//					}
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
