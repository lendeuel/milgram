using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class TrackStateOf
{
	public GameObject file;
	public GameObject notepadHints;
	public GameObject notepadNotes;
}

[Serializable]
public class Forks
{
	public DialogForks suspectPassedOut;
	public DialogForks timeIsUp;
	public DialogForks playerWon;
	public DialogForks onStart;
	public DialogForks threeKeysFound;
}

public class GameManager : MonoBehaviour 
{
	private bool playedPassedOut = false;
	private bool playedTimesUp = false;
	private bool playedPlayerWins = false;
	private bool playedKeys = false;
	private bool playedStart = false;

	public bool censorTheText = false;

	public Forks theForks;
	public TrackStateOf trackStates;
	public bool playStart = false;

	void Start()
	{
		DataHolder.SetStart();
	}
	
	// Update is called once per frame
	void Update () 
	{
		DataHolder.censorText = censorTheText;

		if (trackStates.notepadHints.activeSelf || trackStates.notepadNotes.activeSelf)
		{
			DataHolder.notepadOpen = true;
		}
		else if (!trackStates.notepadHints.activeSelf && !trackStates.notepadNotes.activeSelf)
		{
			DataHolder.notepadOpen = false;
		}

		DataHolder.fileOpen = trackStates.file.activeSelf;

		//Debug.Log("" + DataHolder.notepadOpen + "" + DataHolder.fileOpen);

		if (!playedStart && playStart)
		{
			theForks.onStart.takeAction();

			playedStart = true;
		}

		else if (GetComponent<StatSystem>().GetValueForStat(StatSystem.Stats.health) <= 0 && !playedPassedOut)
		{
			theForks.suspectPassedOut.takeAction();

			playedPassedOut = true;
		}

		else if (DataHolder.isGameOver && !playedTimesUp)
		{
			theForks.timeIsUp.takeAction();

			playedTimesUp = true;  
		}

		else if (DataHolder.locationsFound == 3 && !playedPlayerWins)
		{
			theForks.playerWon.takeAction();

			playedPlayerWins = true;  
		}

		else if (DataHolder.keysFound == 3 && !playedKeys)
		{
			theForks.threeKeysFound.takeAction();
			
			playedKeys = true;  
		}
	}
}
