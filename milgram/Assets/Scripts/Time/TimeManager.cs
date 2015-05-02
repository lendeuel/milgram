using UnityEngine;
using System.Collections;
using System;
using System.Timers;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
	private AudioSource source;

	public AudioClip hourChange;
	private int lastDay;
	public AudioClip dayChange;
	private int lastHour;

	public int penaltyForSkipping = 7;

	public int spacing = 2;
	private string theSpaces = "";

	public float defaultIntervalInSeconds = .5f;
	private double defaultIntervalInMilliseconds = 0;
	
	public int startingDays = 0;
	public int startingHours = 0;
	public int startingMinutes = 0;
	
	//public Material[] numbers = new Material[10];
	
	public Text[] children = new Text[3];
	
	private TimeSpan ts;
	private Timer t;
	
	private Boolean needsUpdate = false;
	
	// Use this for initialization
	void Start () 
	{
		source = gameObject.GetComponent<AudioSource>();

		source.volume = DataHolder.sfxVolume;

		for (int i = 0; i < spacing; i++)
		{
			theSpaces += " ";
		}

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

		lastDay = startingDays;
		lastHour = startingHours;

		SetNumbers();
		
		t = new Timer(defaultIntervalInMilliseconds);
		t.Elapsed += OnTimedEvent;
		t.Start();
	}
	
	void Update()
	{
		if (needsUpdate)
		{
			//Debug.Log("In Needs Update");
			SetNumbers();
		}
	}
	
	// This is called every time defaultIntervalInSeconds has passed.  Each time it is called, a minute is taken off the time left.
	void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		if (!DataHolder.isGameOver) SubtractTime(0,0,1);
		else t.Close();
	}

	public void DialogueSkipped()
	{
		SubtractTime(0,0,penaltyForSkipping);
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

		if (lastDay != ts.Days)
		{
			lastDay = ts.Days;

			if (dayChange != null)
			{
				source.clip = dayChange;
				source.Play();
			}
		}

		if (lastHour != ts.Hours)
		{
			lastHour = ts.Hours;

			if (hourChange != null)
			{
				source.clip = hourChange;
				source.Play();
			}
		}

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

		//Debug.Log(startingDays + " " + startingHours + " " + startingMinutes);

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

		children[0].text = day1 + theSpaces + day2;

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
		
		children[1].text = hour1 + theSpaces + hour2;

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
		
		children[2].text = minute1 + theSpaces + minute2;
		
		needsUpdate = false;
		
		if (startingDays <= 0 && startingHours <= 0 && startingMinutes <= 0)
		{
			DataHolder.isGameOver = true;
		}
	}
	
	void OnApplicationQuit()
	{
		if (!DataHolder.isGameOver) t.Close();
	}
}

// Original TimeManager (NonUI Friendly)

//using UnityEngine;
//using System.Collections;
//using System;
//using System.Timers;
//public class TimeManager : MonoBehaviour
//{
//	public float defaultIntervalInSeconds = 60;
//	private double defaultIntervalInMilliseconds = 0;
//
//	public int startingDays = 0;
//	public int startingHours = 0;
//	public int startingMinutes = 0;
//
//	public Material[] numbers = new Material[10];
//
//	public MeshRenderer[] children = new MeshRenderer[6];
//
//	private TimeSpan ts;
//	private Timer t;
//
//	private Boolean needsUpdate = false;
//
//	// Use this for initialization
//	void Start () 
//	{
//		// Convert to milliseconds
//		defaultIntervalInMilliseconds = defaultIntervalInSeconds * 1000;
//
//		if (startingMinutes >= 60)
//		{
//			startingHours += startingMinutes/60;
//			startingMinutes = startingMinutes%60;
//		}
//
//		if (startingHours >= 24)
//		{
//			startingDays += startingHours/24;
//			startingHours = startingHours%24;
//		}
//
//		ts = new TimeSpan(startingDays, startingHours, startingMinutes, 0);
//
//		SetNumbers();
//
//		t = new Timer(defaultIntervalInMilliseconds);
//		t.Elapsed += OnTimedEvent;
//		t.Start();
//	}
//
//	void Update()
//	{
//		if (needsUpdate)
//		{
//			SetNumbers();
//		}
//	}
//
//	// This is called every time defaultIntervalInSeconds has passed.  Each time it is called, a minute is taken off the time left.
//	void OnTimedEvent(object source, ElapsedEventArgs e)
//	{
//		if (!DataHolder.isGameOver) SubtractTime(0,0,1);
//		else t.Close();
//	}
//
//	public void SubtractTime(int days, int hours, int minutes)
//	{
//		if (minutes >= 60)
//		{
//			hours -= minutes/60;
//			minutes = minutes%60;
//		}
//		
//		if (hours >= 24)
//		{
//			days -= hours/24;
//			hours = hours%24;
//		}
//
//		ts = ts.Subtract(new TimeSpan(days, hours, minutes, 0, 0));
//
//		startingDays = ts.Days;
//		startingHours = ts.Hours;
//		startingMinutes = ts.Minutes;
//
//		needsUpdate = true;
//	}
//
//	void SetNumbers()
//	{
//		startingDays = ts.Days;
//		startingHours = ts.Hours;
//		startingMinutes = ts.Minutes;
//
//
//		// Set days
//		char[] daysSplit = startingDays.ToString().ToCharArray();
//		int day1 = 0;
//		int day2 = 0;
//		
//		if (daysSplit.Length == 2)
//		{
//			day1 = (int)daysSplit[0] - 48;
//			day2 = (int)daysSplit[1] - 48;
//		}
//		else 
//		{
//			day2 = (int)daysSplit[0] - 48;
//		}
//		
//		children[0].material = numbers[day1];
//		children[1].material = numbers[day2];
//
//
//		// Set hours
//		char[] hoursSplit = startingHours.ToString().ToCharArray();
//		int hour1 = 0;
//		int hour2 = 0;
//		
//		if (hoursSplit.Length == 2)
//		{
//			hour1 = (int)hoursSplit[0] - 48;
//			hour2 = (int)hoursSplit[1] - 48;
//		}
//		else 
//		{
//			hour2 = (int)hoursSplit[0] - 48;
//		}
//		
//		children[2].material = numbers[hour1];
//		children[3].material = numbers[hour2];
//
//
//		// Set minutes
//		char[] minutesSplit = startingMinutes.ToString().ToCharArray();
//		int minute1 = 0;
//		int minute2 = 0;
//		
//		if (minutesSplit.Length == 2)
//		{
//			minute1 = (int)minutesSplit[0] - 48;
//			minute2 = (int)minutesSplit[1] - 48;
//		}
//		else 
//		{
//			minute2 = (int)minutesSplit[0] - 48;
//		}
//		
//		children[4].material = numbers[minute1];
//		children[5].material = numbers[minute2];
//
//		needsUpdate = false;
//
//		if (startingDays <= 0 && startingHours <= 0 && startingMinutes <= 0) DataHolder.isGameOver = true;
//	
//	}
//
//	void OnApplicationQuit()
//	{
//		if (!DataHolder.isGameOver) t.Close();
//	}
//}
//
//
//
