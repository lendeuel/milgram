using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

[Serializable]
public class NotePage
{
	public int linesAdded = 0;
	public string theText;

	public NotePage(string line)
	{
		AddString(line);
	}

	public void AddString(string line)
	{
		linesAdded++;

		if (linesAdded == 1)
		{
			theText = line;
		}
		else
		{
			theText += "\r\n" + line;
		}
	}
}

public class NotepadManager : MonoBehaviour 
{
	public List<NotePage> theNotePages;
	public int maxLinesPerPage;
	private int currentPage = 0;

	private int viewPage = 0;

	public void AddLine(string line)
	{
		if (theNotePages.Count == 0)
		{
			theNotePages.Add(new NotePage(line));
		}
		else if (theNotePages[currentPage].linesAdded <= maxLinesPerPage)
		{
			theNotePages[currentPage].AddString(line);

		}
		else
		{
			currentPage++;
			theNotePages.Add(new NotePage(line));
		}
	}

	public string SetPage(bool forward)
	{
		if (forward)
		{
			return SetToPage(viewPage+1);
		}
		else
		{
			return SetToPage(viewPage-1);
		}
	}

	public string SetToPage(int pageNumber)
	{
		viewPage = pageNumber;

		if (pageNumber <= -1)
		{
			Debug.Log("At min page.  Page Num: " + pageNumber);
			viewPage = 0;
			return theNotePages[pageNumber+1].theText;
		}
		else if (theNotePages.Count >= pageNumber)
		{
			Debug.Log("At max page. " + (theNotePages.Count) + " Page Num: " + pageNumber);
			viewPage = theNotePages.Count-1;
			//viewPage = pageNumber - 1;
			return theNotePages[pageNumber-1].theText;
		}

		Debug.Log("In bounds.  Page Num: " + pageNumber);
		return theNotePages[pageNumber].theText;
	}
}
