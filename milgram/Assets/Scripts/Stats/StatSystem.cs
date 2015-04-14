using UnityEngine;
using System.Collections.Generic;

public class StatSystem : MonoBehaviour 
{
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

		AddStat (Stats.health, 5.0f);
		AddStat (Stats.willingness, 10.0f);
		AddStat (Stats.willpower, 2.0f);
		AddStat (Stats.tolerance, 7.0f);
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
		stats [stat] += value;
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
