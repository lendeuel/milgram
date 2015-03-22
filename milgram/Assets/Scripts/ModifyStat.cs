using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ModifyStat : ButtonAction
{
	static public int numberOfStats = 4;
	public StatSystem statSystem;
	public StatSystem.Stats[] statsToModify = new StatSystem.Stats[numberOfStats];

	public float[] modifiers = new float[numberOfStats];
	
	public override void takeAction()
	{
		modify ();
	}

	void Start()
	{
		statSystem = FindObjectOfType<StatSystem> ();

		int count = 0;

		statsToModify[0] = StatSystem.Stats.health;
		statsToModify[1] = StatSystem.Stats.willingness;
		statsToModify[2] = StatSystem.Stats.willpower;
		statsToModify[3] = StatSystem.Stats.tolerance;
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("I'm in modify stat collision between tool and drawer. " + col.collider.tag);

		if (col.collider.tag == "Drawer")
		{
			Debug.Log ("I've collided between tool and drawer, and now I'm modifying stats.");
			modify();
		}
	}

	void modify()
	{
		for(int i=0; i<statsToModify.Length; i++)
		{
			statSystem.AddValueToStat(statsToModify[i], modifiers[i]); 
		}

		statSystem.DebugReportStats();
	}
}
