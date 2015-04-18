using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class StatsToModify
{
	public StatSystem.Stats stat;
	public float modifyValue;

	public StatsToModify()
	{

	}
}

public class ModifyStats : ButtonAction
{
	private const int numberOfStats = 4;
	private StatSystem statSystem; 

	public List<StatsToModify> stats;
	
	public override void takeAction()
	{
		modify ();
	}

	void Start()
	{
		statSystem = FindObjectOfType<StatSystem>();
	}
	
	public void modify() 
	{
		foreach (StatsToModify s in stats)
		{
			statSystem.AddValueToStat(s.stat, s.modifyValue);
		}

		statSystem.DebugReportStats();
	}
}

