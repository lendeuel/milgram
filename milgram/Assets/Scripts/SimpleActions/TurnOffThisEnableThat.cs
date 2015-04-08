using UnityEngine;
using System.Collections;

public class TurnOffThisEnableThat : ButtonAction
{
	public GameObject thisGameObject;
	public GameObject thatGameObject;

	public override void takeAction ()
	{
		thisGameObject.SetActive(false);
		thatGameObject.SetActive(true);
	}
}
