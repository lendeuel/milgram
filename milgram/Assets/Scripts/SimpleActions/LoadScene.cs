using UnityEngine;
using System.Collections;

public class LoadScene : ButtonAction
{
	public int index;

	public override void takeAction()
	{
		Debug.Log("Loading");

		Application.LoadLevel(index);
	}
}
