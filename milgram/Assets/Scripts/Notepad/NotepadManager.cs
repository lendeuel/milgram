﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

[Serializable]
public class NotePage
{
	public int linesAdded = 0;

	public List<string> theLines;

	private string assembledLines;

	public NotePage(string line)
	{
		theLines = new List<string>();
		AddString(line);
	}

	public void AddString(string line)
	{
		linesAdded++;

		theLines.Add(line);
	}

	public string AssembleLines()
	{
		linesAdded = theLines.Count;

		assembledLines = theLines[0];

		for (int i = 1; i < theLines.Count; i++)
		{
			assembledLines += "\r\n\r\n" + theLines[i];
		}

		return assembledLines;
	}
}

[Serializable]
public class HintAndFork
{
	public string theString;
	public DialogForks theFork;

	public HintAndFork(string line, DialogForks d)
	{
		theString = line;
		theFork = d;
	}
}

[Serializable]
public class HintPage
{
	public int gameObjectsAdded = 0;
	
	public List<HintAndFork> hintFork;
	
	public HintPage(string line, DialogForks d)
	{
		hintFork = new List<HintAndFork>();

		AddHint(line, d);
	}

	// This needs to determine what gameObject this Hint gets assigned to, only set the text on text, and the action on button action
	public void AddHint(string line, DialogForks d)
	{
		gameObjectsAdded++;

		hintFork.Add(new HintAndFork(line, d));
	}
}

[Serializable]
public class GameObjectHolder
{
	public GameObject element1;
	public GameObject element2;
	public GameObject element3;
	public GameObject element4;
}

public class NotepadManager : MonoBehaviour 
{
	public List<NotePage> theNotePages;
	[NonSerialized]public List<string> theLines;
	public int maxLinesPerPage;
	private int currentNotePage = 0;
	private int viewNotePage = 0;

	public List<HintPage> theHintPages;
	[NonSerialized]public List<HintAndFork> theHintLines;
	public List<GameObject> hintObjects;
	private int maxObjectsPerPage = 4;
	private int currentHintPage = 0;
	private int viewHintPage = 0;
	
	void Start()
	{
		theLines = new List<string>();
		theHintLines = new List<HintAndFork>();

		foreach(NotePage n in theNotePages)
		{
			n.AssembleLines();

			foreach(string s in n.theLines)
			{
				theLines.Add(s);
			}
		}

		currentNotePage = theNotePages.Count - 1;

		for (int i = 0; i < 4; i++)
		{
			hintObjects[i].collider2D.enabled = false;
			hintObjects[i].GetComponent<Text>().text = "";
			hintObjects[i].GetComponent<Button>().action = null;
			hintObjects[i].GetComponent<FadeInFadeOut>().enabled = false;
		}

		for (int i = 0; i < theHintPages.Count; i++)
		{
			AssembleObjects(i);
		}

		currentHintPage = theHintPages.Count - 1;
	}

	void Update()
	{
		//Debug.Log("Current Page: " + currentPage + " View Page: " + viewPage);
		foreach(HintAndFork h in theHintLines)
		{
			if (h.theFork.dialogQueuers.Count == 0)
			{
				RemoveHint(h.theString);
				break;
			}
		}
	}
		
// Hints
	public void SetHintPage(bool forward)
	{
		if (forward)
		{
			SetToHintPage(viewHintPage+1);
			return;
		}
		else
		{
			SetToHintPage(viewHintPage-1);
			return;
		}
	}

	public void SetToHintPage(int pageNumber)
	{
		//Debug.Log("In SetToHintPage: " + pageNumber);
		if (theHintPages.Count == 0)
		{
			//Debug.Log("Returning early.");
			return;
		}

		viewHintPage = pageNumber;
		
		if (pageNumber == -1)
		{
			//Debug.Log("At min.");
			viewHintPage = 0;
			AssembleObjects(0);
			return;
		}
		
		if (pageNumber >= theHintPages.Count)
		{
			//Debug.Log("At max. Count: " + theHintPages.Count + " Page Number: " + pageNumber);
			viewHintPage = theHintPages.Count - 1;
			AssembleObjects(viewHintPage);
			return;
		}
		
		//Debug.Log("In bounds.  Page Num: " + pageNumber);
		AssembleObjects(pageNumber);
		return;
	}

	public void AddHint(string line, DialogForks d)
	{
		if (theHintPages.Count == 0)
		{
			currentHintPage++;
			theHintPages.Add(new HintPage(line, d));
			theHintLines.Add(new HintAndFork(line, d));
		}
		else if (theHintPages[currentHintPage].gameObjectsAdded < maxObjectsPerPage)
		{
			theHintPages[currentHintPage].AddHint(line,d);
			theHintLines.Add(new HintAndFork(line, d));
		}
		else
		{
			currentHintPage++;
			theHintPages.Add(new HintPage(line, d));
			theHintLines.Add(new HintAndFork(line, d));
		}
	}

	public void AddHint2(string line, DialogForks d)
	{
		if (theHintPages.Count == 0)
		{
			currentHintPage++;
			theHintPages.Add(new HintPage(line, d));
		}
		else if (theHintPages[currentHintPage].gameObjectsAdded < maxObjectsPerPage)
		{
			theHintPages[currentHintPage].AddHint(line,d);
		}
		else
		{
			currentHintPage++;
			theHintPages.Add(new HintPage(line, d));
		}
	}

	public void AssembleObjects(int index)
	{
		for (int i = 0; i < theHintPages[index].gameObjectsAdded; i++)
		{
			hintObjects[i].collider2D.enabled = true;
			hintObjects[i].GetComponent<Text>().text = theHintPages[index].hintFork[i].theString;
			hintObjects[i].GetComponent<Button>().action = theHintPages[index].hintFork[i].theFork;

			hintObjects[i].GetComponent<FadeInFadeOut>().enabled = true;

			if (theHintPages[index].hintFork[i].theFork.dialogQueuers.Count != 0)
			{
				hintObjects[i].GetComponent<FadeInFadeOut>().disabled = false;
			}
			else
			{
				hintObjects[i].GetComponent<FadeInFadeOut>().disabled = true;
			}
		}

		if (theHintPages[index].gameObjectsAdded < maxObjectsPerPage)
		{
			for (int i = theHintPages[index].gameObjectsAdded; i < maxObjectsPerPage; i++)
			{
				hintObjects[i].collider2D.enabled = false;
				hintObjects[i].GetComponent<Text>().text = "";
				hintObjects[i].GetComponent<Button>().action = null;
				hintObjects[i].GetComponent<FadeInFadeOut>().disabled = true;
			}
		}
	}

	public void RemoveHint(String hint)
	{
		// Remove the hint
		foreach(HintAndFork l in theHintLines)
		{
			if (l.theString.CompareTo(hint) == 0)
			{
				theHintLines.Remove(l);
				break;
			}
		}
		
		// Reassemble hint pages to fill in missing gap.
		theHintPages = new List<HintPage>();
		
		currentHintPage = 0;
		
		foreach (HintAndFork l in theHintLines)
		{
			AddHint2(l.theString, l.theFork);
		}

		for (int i = 0; i < 4; i++)
		{
			hintObjects[i].collider2D.enabled = false;
			hintObjects[i].GetComponent<Text>().text = "";
			hintObjects[i].GetComponent<Button>().action = null;
			hintObjects[i].GetComponent<FadeInFadeOut>().disabled = true;
		}
	}

// Notes
	public void RemoveNote(String note)
	{
		// Remove the note
		foreach(string l in theLines)
		{
			if (l.CompareTo(note) == 0)
			{
				theLines.Remove(l);
				break;
			}
		}

		// Reassemble note pages to fill in missing gap.
		theNotePages = new List<NotePage>();
		viewNotePage = 0;
		currentNotePage = 0;

		foreach (string l in theLines)
		{
			AddLine2(l);
		}
	}

	public string SetNotepage(bool forward)
	{
		if (forward)
		{
			return SetToPage(viewNotePage+1);
		}
		else
		{
			return SetToPage(viewNotePage-1);
		}
	}
	
	public string SetToPage(int pageNumber)
	{
		if (theNotePages.Count == 0)
		{
			return "";
		}

		viewNotePage = pageNumber;

		if (pageNumber == -1)
		{
			//Debug.Log("At min.");
			viewNotePage = 0;
			return theNotePages[0].AssembleLines();
		}

		if (pageNumber >= theNotePages.Count)
		{
			//Debug.Log("At max. Count: " + theNotePages.Count + " Page Number: " + pageNumber);
			viewNotePage = theNotePages.Count - 1;
			return theNotePages[viewNotePage].AssembleLines();
		}

		//Debug.Log("In bounds.  Page Num: " + pageNumber);
		return theNotePages[pageNumber].AssembleLines();
	}
	
	public void AddLine(string line)
	{
		if (theNotePages.Count == 0)
		{
			theNotePages.Add(new NotePage(line));
			theLines.Add(line);
		}
		else if (theNotePages[currentNotePage].linesAdded < maxLinesPerPage)
		{
			theNotePages[currentNotePage].AddString(line);
			theLines.Add(line);
		}
		else
		{
			currentNotePage++;
			theNotePages.Add(new NotePage(line));
			theLines.Add(line);
		}
	}

	public void AddLine2(string line)
	{
		if (theNotePages.Count == 0)
		{
			theNotePages.Add(new NotePage(line));
		}
		else if (theNotePages[currentNotePage].linesAdded < maxLinesPerPage)
		{
			theNotePages[currentNotePage].AddString(line);
		}
		else
		{
			currentNotePage++;
			theNotePages.Add(new NotePage(line));
		}
	}
}
