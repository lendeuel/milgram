using UnityEngine;
using System.Collections;

public class RetractMoveTos : ButtonAction 
{
	public override void takeAction()
	{
		Debug.Log("Moving");

		MoveToTools[] m = GameObject.FindObjectsOfType<MoveToTools>();

		foreach (MoveToTools moves in m)
		{
			moves.MoveToOriginalLocation();
		}
	}
}
