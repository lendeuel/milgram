using UnityEngine;
using System.Collections;

public class ModifyStat : ButtonAction
{
	public StatSystem statSystem;
	public StatSystem.Stats[] statsToModify;
	public float[] modifiers;

	public override void takeAction()
	{
		modify ();
	}

	void OnMouseUp()
	{
		modify ();
	}

	void modify()
	{
		for(int i=0; i<statsToModify.Length; i++)
		{
			statSystem.AddValueToStat(statsToModify[i], modifiers[i]); 
		}
	}
}
