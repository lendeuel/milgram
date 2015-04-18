using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class StartingStats
{
	public float health;
	public float willingness;
	public float willpower;
	public float tolerance;
}

public class StatSystem : MonoBehaviour 
{
	public StartingStats startingStats;

	public enum Stats
	{
		health,
		willingness,
		willpower,
		tolerance
	};

	private Dictionary<Stats, float> stats;

	void Start () 
	{
		stats = new Dictionary<Stats, float> ();

		AddStat (Stats.health, startingStats.health);
		AddStat (Stats.willingness, startingStats.willingness);
		AddStat (Stats.willpower, startingStats.willpower);
		AddStat (Stats.tolerance, startingStats.tolerance);

		DebugReportStats();
	}

	void Update()
	{
//		Debug.Log ("Health:" + GetValueForStat (Stats.health));
//		Debug.Log ("Willingness:" + GetValueForStat (Stats.willingness));
//		Debug.Log ("Willpower:" + GetValueForStat (Stats.willpower));
//		Debug.Log ("Tolerance:" + GetValueForStat (Stats.tolerance));
	}

	public void AddValueToStat (Stats stat, float value)
	{
		stats[stat] += value;
	}

	public void AddStat(Stats stat, float value)
	{		
		if (!stats.ContainsKey (stat)) 
		{
			stats.Add(stat, value);
		}
		else
		{
			stats.Add(stat, stats[stat]+value);
		}
	}

	public float GetValueForStat(Stats stat)
	{
		if(!stats.ContainsKey(stat))
		{
			return 0;
		}

		return stats [stat];
	}

	public void SetStats(Dictionary<Stats, float> newDic)
	{
		DebugReportStats();

		stats = newDic;
	}

	public Dictionary<Stats, float> GetStats()
	{
		return stats;
	}

	public void DebugReportStats()
	{
		Debug.Log ("Health:" + GetValueForStat (Stats.health));
		Debug.Log ("Willingness:" + GetValueForStat (Stats.willingness));
		Debug.Log ("Willpower:" + GetValueForStat (Stats.willpower));
		Debug.Log ("Tolerance:" + GetValueForStat (Stats.tolerance));
	}
}
