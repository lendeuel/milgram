// The stats appear in the modifiers array as follows:
// Health
// Willingness
// Willpower
// Tolerance

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ModifyStat : ButtonAction
{
	static int numberOfStats = 4;
	public StatSystem statSystem;
	StatSystem.Stats[] statsToModify = new StatSystem.Stats[numberOfStats];

	public float[] modifiers = new float[numberOfStats];
	
	public override void takeAction()
	{
		modify ();
	}

	void Start()
	{
		statSystem = FindObjectOfType<StatSystem> ();

		statsToModify[0] = StatSystem.Stats.health;
		statsToModify[1] = StatSystem.Stats.willingness;
		statsToModify[2] = StatSystem.Stats.willpower;
		statsToModify[3] = StatSystem.Stats.tolerance;
	}

	void OnCollisionEnter(Collision col)
	{
		//Debug.Log ("Modify Stat.  OnCollisionEnter. Tag: " + col.collider.tag);

		if (col.collider.tag == "Drawer")
		{
			//Debug.Log ("I've collided between tool and drawer on Enter, and now I'm modifying stats.");
			//modify();
		}
	}

	void OnCollisionExit(Collision col)
	{
		//Debug.Log ("Modify Stat.  OnCollisionExit. Tag: " + col.collider.tag);
		
		if (col.collider.tag == "Drawer")
		{
			//Debug.Log ("I've collided between tool and drawer on Exit, and now I'm modifying stats.");
			//modify();
		}
	}
	
	public void modify() 
	{
		for(int i=0; i<statsToModify.Length; i++)
		{
			statSystem.AddValueToStat(statsToModify[i], modifiers[i]); 
		}

		statSystem.DebugReportStats();
	}
	
	public void modifyStats(float health, float willpower, float willingness, float tolerance) 
	{
		statSystem.AddValueToStat(StatSystem.Stats.health, health);
		statSystem.AddValueToStat(StatSystem.Stats.willingness, willingness);
		statSystem.AddValueToStat(StatSystem.Stats.willpower, willpower);
		statSystem.AddValueToStat(StatSystem.Stats.tolerance, tolerance);

		statSystem.DebugReportStats();
	}
}

