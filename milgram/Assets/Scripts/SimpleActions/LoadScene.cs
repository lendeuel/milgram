using UnityEngine;
using System.Collections;

public class LoadScene : ButtonAction
{
	public bool useIndex;
	public bool useString;
	public int index;
	public string name;

	public override void takeAction()
	{
		Debug.Log("Loading");

		if (useIndex)
		{
			if (index != -1)
			{
				GameObject.FindObjectOfType<SceneFade>().LoadScene(index);
			}
			else
			{
				Application.Quit();
			}
		}

		if(useString)
		{
			GameObject.FindObjectOfType<SceneFade>().LoadScene(name);
		}
	}

	public void Load(int lvlIndex)
	{
		GameObject.FindObjectOfType<SceneFade>().LoadScene(index);
	}

	public void Load(string name)
	{
		GameObject.FindObjectOfType<SceneFade>().LoadScene(name);
	}
}