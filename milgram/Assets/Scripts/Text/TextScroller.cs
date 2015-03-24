using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextScroller : ButtonAction
{
	public interface TextScrollerEndedResponder
	{
		void textScrollerEnded();
	}

	public bool destroyOnComplete=true;
	public string[] linesToLoad;
	public float lettersPerSecond;
	public MonoBehaviour endedResponse;
	private float mTimeElapsed=0;
	private int index = 0;
	private bool displayAll=false;
	private List<string> lines;

	void Start()
	{
		foreach(string s in linesToLoad)
		{
			lines.Add (s);
		}
	}

	void Update()
	{
		DataHolder.allowInteractions = false;
		if(index>lines.Count+1)
		{
			if(endedResponse is TextScrollerEndedResponder)
			{
				(endedResponse as TextScrollerEndedResponder).textScrollerEnded();
			}
			else
			{
				Debug.Log("Ended responder must implement TextScrollerEndedResponder");
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
				GetComponent<Text> ().text = lines[index];
			}
			else
			{
				mTimeElapsed += Time.deltaTime;
				int stoppingPoint = (int)(mTimeElapsed * lettersPerSecond);
				if(stoppingPoint>lines[index].Length)
				{
					stoppingPoint=lines[index].Length;
					displayAll=true;
				}
				string text = lines [index].Substring (0, stoppingPoint);
				GetComponent<Text> ().text = text;
			}
		}
	}

	public void addString(string s)
	{
		DataHolder.allowInteractions = false;
		lines.Add (s);
	}

	public override void takeAction()
	{
		Debug.Log ("box clicked");
		if(displayAll)
		{
			mTimeElapsed = 0;
			index++;
			displayAll=false;
		}
		else
		{
			displayAll=true;
		}
	}
}
