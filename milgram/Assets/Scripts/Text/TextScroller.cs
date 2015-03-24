using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScroller : ButtonAction
{
	public interface TextScrollerEndedResponder
	{
		void textScrollerEnded();
	}

	public string[] textToLoad;
	public float lettersPerSecond;
	public MonoBehaviour endedResponse;
	private float mTimeElapsed=0;
	private int index = 0;
	private bool displayAll=false;

	void Update()
	{
		DataHolder.allowInteractions = false;
		if(index>textToLoad.Length+1)
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
			Destroy(gameObject);
		}
		else
		{
			if(displayAll)
			{
				GetComponent<Text> ().text = textToLoad[index];
			}
			else
			{
				mTimeElapsed += Time.deltaTime;
				int stoppingPoint = (int)(mTimeElapsed * lettersPerSecond);
				if(stoppingPoint>textToLoad[index].Length)
				{
					stoppingPoint=textToLoad[index].Length;
					displayAll=true;
				}
				string text = textToLoad [index].Substring (0, stoppingPoint);
				GetComponent<Text> ().text = text;
			}
		}
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
