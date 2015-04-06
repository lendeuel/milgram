using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public DialogForks suspectPassedOut;
	public DialogForks timeIsUp;
	public DialogForks playerWon;

	// Update is called once per frame
	void Update () 
	{
		if (GetComponent<StatSystem>().GetValueForStat(StatSystem.Stats.health) == 0)
		{
			suspectPassedOut.takeAction();

			GetComponent<StatSystem>().AddValueToStat(StatSystem.Stats.health, -1f);
		}
		else if (DataHolder.isGameOver)
		{
			timeIsUp.takeAction();

			DataHolder.isGameOver = false;  // temporary
		}
		else if (DataHolder.locationsFound == 3)
		{
			playerWon.takeAction();

			DataHolder.locationsFound++;  // temporary
		}
	}
}
