using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public DialogForks suspectPassedOut;
	public DialogForks timeIsUp;
	public DialogForks playerWon;
	public DialogForks onStart;
	public DialogForks threeKeysFound;

	public bool playStart = false;

	private bool playedPassedOut = false;
	private bool playedTimesUp = false;
	private bool playedPlayerWins = false;
	private bool playedKeys = false;
	private bool playedStart = false;

	void Start()
	{
		//onStart.takeAction();
	}

	// Update is called once per frame
	void Update () 
	{
		if (!playedStart && playStart)
		{
			onStart.takeAction();

			playedStart = true;
		}

		else if (GetComponent<StatSystem>().GetValueForStat(StatSystem.Stats.health) <= 0 && !playedPassedOut)
		{
			suspectPassedOut.takeAction();

			playedPassedOut = true;
		}

		else if (DataHolder.isGameOver && !playedTimesUp)
		{
			timeIsUp.takeAction();

			playedTimesUp = true;  
		}

		else if (DataHolder.locationsFound == 3 && !playedPlayerWins)
		{
			playerWon.takeAction();

			playedPlayerWins = true;  
		}

		else if (DataHolder.keysFound == 3 && !playedKeys)
		{
			threeKeysFound.takeAction();
			
			playedKeys = true;  
		}
	}
}
