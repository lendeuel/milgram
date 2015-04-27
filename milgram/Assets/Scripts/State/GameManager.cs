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
	public bool startIsMultipleAction = false;
	public bool startIsDialogFork = false;

	void Start()
	{
		DataHolder.SetStart();

		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().volume = DataHolder.musicVolume;
	}

	public void Reset()
	{
		DataHolder.Reset();
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Load(int lvlIndex)
	{
		DataHolder.Reset();
		gameObject.GetComponent<LoadScene>().Load (lvlIndex);
	}
	
	public void Load(string name)
	{
		DataHolder.Reset();
		gameObject.GetComponent<LoadScene>().Load (name);
	}

	public void SetMusicVolume(float value)
	{
		DataHolder.musicVolume = value;
	}

	public void SetSFXVolume(float value)
	{
		DataHolder.sfxVolume = value;
	}

	public void SetCensor(bool value)
	{
		DataHolder.censorText = value;
	}

	// Update is called once per frame
	void Update () 
	{
		//DataHolder.censorText = censorTheText;

		//Debug.Log("Censor: " + DataHolder.censorText + " Music Volume: " + DataHolder.musicVolume + " SFX Volume: " + DataHolder.sfxVolume);

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

		if (!playedStart && playStart && startIsDialogFork)
		{
			theForks.onStart.takeAction();

			playedStart = true;
		}

		if (!playedStart && playStart && startIsMultipleAction)
		{
			gameObject.GetComponent<ButtonMultipleActions>().Run();
			
			playedStart = true;
		}

		if (GetComponent<StatSystem>().GetValueForStat(StatSystem.Stats.health) >= 10 && !playedPassedOut)
		{
			theForks.suspectPassedOut.takeAction();

			playedPassedOut = true;
		}

		if (DataHolder.isGameOver && !playedTimesUp)
		{
			theForks.timeIsUp.takeAction();

			playedTimesUp = true;  
		}

		if (DataHolder.locationsFound == 3 && !playedPlayerWins)
		{
			theForks.playerWon.takeAction();

			playedPlayerWins = true;  
		}

		if (DataHolder.keysFound == 3 && !playedKeys)
		{
			theForks.threeKeysFound.takeAction();
			
			playedKeys = true;  
		}
	}
}
