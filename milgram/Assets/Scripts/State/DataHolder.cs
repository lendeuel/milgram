using UnityEngine;
using System.Collections;

public class DataHolder
{
	public static bool allowInteractions=true;
	public static bool isGameOver = false;
	public static bool fileOpen = false;
	public static bool notepadOpen = false;
	public static int keysFound = 0;
	public static int locationsFound = 0;
	public static int hintsFound = 0;
	public static bool censorText = false;

	// These are only needed if we keep the restart button
	private static bool allowInteractionsI;
	private static bool isGameOverI;
	private static bool fileOpenI;
	private static bool notepadOpenI;
	private static int keysFoundI;
	private static int locationsFoundI;
	private static int hintsFoundI;
	private static bool censorTextI;

	public static void SetStart()
	{
		allowInteractionsI = allowInteractions;
		isGameOverI = isGameOver;
		fileOpenI = fileOpen;
		notepadOpenI = notepadOpen;
		keysFoundI = keysFound;
		locationsFoundI = locationsFound;
		hintsFoundI = hintsFound;
		censorTextI = censorText;
	}

	public static void Reset()
	{
		allowInteractions = allowInteractionsI;
		isGameOver = isGameOverI;
		fileOpen = fileOpenI;
		notepadOpen = notepadOpenI;
		keysFound = keysFoundI;
		locationsFound = locationsFoundI;
		hintsFound = hintsFoundI;
		censorText = censorTextI;
	}
}
