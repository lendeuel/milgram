using UnityEngine;
using System.Collections;

public class LoadScene : ButtonAction
{
	public int index;

	public override void takeAction()
	{
		Debug.Log("Loading");

		if (index != -1)
		{
			Application.LoadLevel(index);
		}
		else
		{
			Application.Quit();
		}
	}
}
