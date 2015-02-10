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

	void Awake () 
	{
		stats = new Dictionary<Stats, float> ();	
	}
	
	public void AddValueToStat(Stats stat, float value)
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
}
