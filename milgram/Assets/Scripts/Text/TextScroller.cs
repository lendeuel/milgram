using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[Serializable]
public class LineAndSpeaker
{
	public string line;
	public Characters speaker;
}

[Serializable]
public class CharacterToMaterial
{
	public Characters character;
	public Sprite material;
}

public class TextScroller : ButtonAction
{
	public interface TextScrollerEndedResponder
	{
		void textScrollerEnded();
	}

	public bool destroyOnComplete=true;
	public LineAndSpeaker[] linesToLoad;
	public float lettersPerSecond=3;
	public MonoBehaviour endedResponse;
	private float mTimeElapsed=0;
	private int index = 0;
	private bool displayAll=false;
	private List<LineAndSpeaker> lines;
	public CharacterToMaterial[] characterToMaterialMapping;
	private Image chatWindow;
	private bool hasEnded=false;

	void Start()
	{

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
					chatWindow.sprite = c.material;
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
				Debug.Log("good im here");
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
				GetComponent<Text> ().text = lines[index].line;
			}
			else
			{
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
		Debug.Log (lines.Count);
		DataHolder.allowInteractions = false;
		foreach(LineAndSpeaker l in s)
		{
			lines.Add (l);
		}
		Debug.Log (lines.Count);
		foreach(LineAndSpeaker l in lines)
		{
			Debug.Log(l.line);
		}
		if(index>lines.Count)
		{
			mTimeElapsed = 0;
		}
	}

	public override void takeAction()
	{
		Debug.Log ("box clicked");
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
							Debug.Log("changing sprite");
							chatWindow.sprite = c.material;
						}
					}
				}
				displayAll=false;
			}
			else
			{
				displayAll=true;
			}
		}
	}

	void OnMouseUp()
	{
		//takeAction ();
	}
}
