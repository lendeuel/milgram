﻿using UnityEngine;
using System.Collections;

public class DataHolder
{
	public static bool allowInteractions=true;
	public static bool isGameOver = false;
	public static bool fileOpen = false;
	public static bool notepadOpen = false;
	public static bool toolRackMoving = false;
	public static int keysFound = 0;
	public static int locationsFound = 0;

	// These are only needed if we keep the restart button
	private static bool allowInteractionsI;
	private static bool isGameOverI;
	private static bool fileOpenI;
	private static bool notepadOpenI;
	private static bool toolRackMovingI;
	private static int keysFoundI;
	private static int locationsFoundI;

	public static void SetStart()
	{
		allowInteractionsI = allowInteractions;
		isGameOverI = isGameOver;
		fileOpenI = fileOpen;
		notepadOpenI = notepadOpen;
		toolRackMovingI = toolRackMoving;
		keysFoundI = keysFound;
		locationsFoundI = locationsFound;
	}

	public static void Reset()
	{
		allowInteractions = allowInteractionsI;
		isGameOver = isGameOverI;
		fileOpen = fileOpenI;
		notepadOpen = notepadOpenI;
		toolRackMoving = toolRackMovingI;
		keysFound = keysFoundI;
		locationsFound = locationsFoundI;
	}
}
