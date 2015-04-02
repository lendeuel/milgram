using UnityEngine;
using System.Collections;
using System;
using System.Timers;
public class TimeManager : MonoBehaviour
{
	public float defaultIntervalInSeconds = 60;
	private double defaultIntervalInMilliseconds = 0;

	public int startingDays = 0;
	public int startingHours = 0;
	public int startingMinutes = 0;

	public Material[] numbers = new Material[10];

	public MeshRenderer[] children = new MeshRenderer[6];

	private TimeSpan ts;
	private Timer t;

	private Boolean needsUpdate = false;

	// Use this for initialization
	void Start () 
	{
		// Convert to milliseconds
		defaultIntervalInMilliseconds = defaultIntervalInSeconds * 1000;

		if (startingMinutes >= 60)
		{
			startingHours += startingMinutes/60;
			startingMinutes = startingMinutes%60;
		}

		if (startingHours >= 24)
		{
			startingDays += startingHours/24;
			startingHours = startingHours%24;
		}

		ts = new TimeSpan(startingDays, startingHours, startingMinutes, 0);

		SetNumbers();

		t = new Timer(defaultIntervalInMilliseconds);
		t.Elapsed += OnTimedEvent;
		t.Start();
	}

	void Update()
	{
		if (needsUpdate)
		{
			SetNumbers();
		}
	}

	// This is called every time defaultIntervalInSeconds has passed.  Each time it is called, a minute is taken off the time left.
	void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		SubtractTime(0,0,1);
	}

	public void SubtractTime(int days, int hours, int minutes)
	{
		if (minutes >= 60)
		{
			hours -= minutes/60;
			minutes = minutes%60;
		}
		
		if (hours >= 24)
		{
			days -= hours/24;
			hours = hours%24;
		}

		ts = ts.Subtract(new TimeSpan(days, hours, minutes, 0, 0));

		startingDays = ts.Days;
		startingHours = ts.Hours;
		startingMinutes = ts.Minutes;

		needsUpdate = true;
	}

	void SetNumbers()
	{
		startingDays = ts.Days;
		startingHours = ts.Hours;
		startingMinutes = ts.Minutes;


		// Set days
		char[] daysSplit = startingDays.ToString().ToCharArray();
		int day1 = 0;
		int day2 = 0;
		
		if (daysSplit.Length == 2)
		{
			day1 = (int)daysSplit[0] - 48;
			day2 = (int)daysSplit[1] - 48;
		}
		else 
		{
			day2 = (int)daysSplit[0] - 48;
		}
		
		children[0].material = numbers[day1];
		children[1].material = numbers[day2];


		// Set hours
		char[] hoursSplit = startingHours.ToString().ToCharArray();
		int hour1 = 0;
		int hour2 = 0;
		
		if (hoursSplit.Length == 2)
		{
			hour1 = (int)hoursSplit[0] - 48;
			hour2 = (int)hoursSplit[1] - 48;
		}
		else 
		{
			hour2 = (int)hoursSplit[0] - 48;
		}
		
		children[2].material = numbers[hour1];
		children[3].material = numbers[hour2];


		// Set minutes
		char[] minutesSplit = startingMinutes.ToString().ToCharArray();
		int minute1 = 0;
		int minute2 = 0;
		
		if (minutesSplit.Length == 2)
		{
			minute1 = (int)minutesSplit[0] - 48;
			minute2 = (int)minutesSplit[1] - 48;
		}
		else 
		{
			minute2 = (int)minutesSplit[0] - 48;
		}
		
		children[4].material = numbers[minute1];
		children[5].material = numbers[minute2];


		needsUpdate = false;
	}

	void OnApplicationQuit()
	{
		t.Stop();
		t.Dispose();
	}
}
