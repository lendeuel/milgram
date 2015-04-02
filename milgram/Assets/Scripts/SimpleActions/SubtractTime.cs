using UnityEngine;
using System.Collections;

public class SubtractTime : ButtonAction
{
	public int days;
	public int hours;
	public int minutes;

	private TimeManager t;

	void Start()
	{
		t = FindObjectOfType<TimeManager>();
	}

	public override void takeAction()
	{
		t.SubtractTime(days,hours,minutes);
	}
}
