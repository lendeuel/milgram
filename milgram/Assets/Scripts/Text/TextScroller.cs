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
	public Material material;
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

	void Start()
	{
		lines = new List<LineAndSpeaker>();

		foreach(LineAndSpeaker s in linesToLoad)
		{
			lines.Add (s);
		}

		int tempCount = 0;

		foreach(CharacterToMaterial c in characterToMaterialMapping)
		{
			if(c.character == lines[tempCount].speaker)
			{
				GetComponent<MeshRenderer> ().material = c.material;

				tempCount++;
			}
		}
	}

	void Update()
	{
		DataHolder.allowInteractions = false;
		if(index>lines.Count+1)
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
		DataHolder.allowInteractions = false;
		lines.Add (s);
		if(index>lines.Count)
		{
			mTimeElapsed = 0;
		}
	}

	public override void takeAction()
	{
		Debug.Log ("box clicked");
		if(displayAll)
		{
			mTimeElapsed = 0;
			index++;
			foreach(CharacterToMaterial c in characterToMaterialMapping)
			{
				if(c.character == lines[index].speaker)
				{
					GetComponent<MeshRenderer> ().material = c.material;
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
