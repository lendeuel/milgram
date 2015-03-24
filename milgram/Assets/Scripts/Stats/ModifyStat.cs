using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ModifyStat : ButtonAction
{
	private const int numberOfStats = 4;
	public StatSystem statSystem; 

	public StatSystem.Stats[] statsToModify = new StatSystem.Stats[numberOfStats]
	{
		StatSystem.Stats.health,
		StatSystem.Stats.willingness,
		StatSystem.Stats.willpower,
		StatSystem.Stats.tolerance
	};

	public float[] modifiers = new float[numberOfStats];
	
	public override void takeAction()
	{
		modify ();
	}

	void Start()
	{
		statSystem = FindObjectOfType<StatSystem> ();
	}
	
	public void modify() 
	{
		for(int i=0; i<statsToModify.Length; i++)
		{
			statSystem.AddValueToStat(statsToModify[i], modifiers[i]); 
		}

		statSystem.DebugReportStats();
	}
}

