using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class StatsToModify
{
	public float health = 0;
	public float willingness = 0;
	public float willpower = 0;
	public float tolerance = 0;
}

public class ModifyStats : ButtonAction
{
	private const int numberOfStats = 4;
	private StatSystem statSystem; 

	public StatsToModify stats;
	
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
		statSystem.AddValueToStat(StatSystem.Stats.health, stats.health);
		statSystem.AddValueToStat(StatSystem.Stats.willpower, stats.willpower);
		statSystem.AddValueToStat(StatSystem.Stats.willingness, stats.willingness);
		statSystem.AddValueToStat(StatSystem.Stats.tolerance, stats.tolerance);

		statSystem.DebugReportStats();
	}
}

