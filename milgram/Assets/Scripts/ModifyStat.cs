using UnityEngine;
using System.Collections;

public class ModifyStat : MonoBehaviour 
{
	private StatSystem suspectsStats;
	public float healthModify;
	public float willingnessModify;
	public float willpowerModify;
	public float toleranceModify;
	
	// Use this for initialization
	void Start () 
	{
		suspectsStats = GameObject.FindGameObjectWithTag ("Suspect").GetComponent<StatSystem>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseUp()
	{
		suspectsStats.AddValueToStat (StatSystem.Stats.health, healthModify);
		suspectsStats.AddValueToStat (StatSystem.Stats.willingness, willingnessModify);
		suspectsStats.AddValueToStat (StatSystem.Stats.willpower, willpowerModify);
		suspectsStats.AddValueToStat (StatSystem.Stats.tolerance, toleranceModify);

	}
}
