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

	//public bool censorTheText = false;

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

	public void LoadThisScene()
	{
		DataHolder.Reset();
		gameObject.GetComponent<LoadScene>().LoadThisScene();
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

		if (GameObject.FindGameObjectWithTag("Map").GetComponent<MoveToTools>().movedOut)
		{
			Debug.Log("MapTrue");
			
			GameObject.FindGameObjectWithTag("StaticFile").GetComponent<BoxCollider2D>().enabled = false;
		}
		else
		{
			Debug.Log("MapFalse");
			
			GameObject.FindGameObjectWithTag("StaticFile").GetComponent<BoxCollider2D>().enabled = true;
		}


		if (GameObject.FindGameObjectWithTag("ToolRack").GetComponent<MoveToTools>().movedOut)
		{
			Debug.Log("ToolRackTrue");

			GameObject.FindGameObjectWithTag("StaticMap").GetComponent<BoxCollider2D>().enabled = false;
		}
		else
		{
			Debug.Log("ToolRackFalse");

			GameObject.FindGameObjectWithTag("StaticMap").GetComponent<BoxCollider2D>().enabled = true;
		}

		if (trackStates.notepadHints.activeSelf || trackStates.notepadNotes.activeSelf)
		{
			Debug.Log("NotepadTrue");

			DataHolder.notepadOpen = true;

			GameObject.FindGameObjectWithTag("StaticFile").GetComponent<BoxCollider2D>().enabled = false;
		}
		else if (!trackStates.notepadHints.activeSelf && !trackStates.notepadNotes.activeSelf && !GameObject.FindGameObjectWithTag("Map").GetComponent<MoveToTools>().movedOut)
		{
			Debug.Log("NotepadFalse");

			DataHolder.notepadOpen = false;

			GameObject.FindGameObjectWithTag("StaticFile").GetComponent<BoxCollider2D>().enabled = true;
		}

		DataHolder.fileOpen = trackStates.file.activeSelf;

		if (DataHolder.fileOpen)
		{
			Debug.Log("FileTrue");

			GameObject.FindGameObjectWithTag("Notepad").GetComponent<BoxCollider2D>().enabled = false;
		}
		else
		{
			Debug.Log("FileFalse");

			GameObject.FindGameObjectWithTag("Notepad").GetComponent<BoxCollider2D>().enabled = true;
		}



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
