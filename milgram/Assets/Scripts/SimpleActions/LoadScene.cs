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
				Application.LoadLevel(index);
			}
			else
			{
				Application.Quit();
			}
		}

		if(useString)
		{
			Application.LoadLevel(name);
		}
	}

	public void Load(int lvlIndex)
	{
		Application.LoadLevel(lvlIndex);
	}

	public void Load(string name)
	{
		Application.LoadLevel(name);
	}
}
