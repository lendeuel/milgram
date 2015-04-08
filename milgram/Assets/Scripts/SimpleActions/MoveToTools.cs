﻿using UnityEngine;
using System.Collections;

public class MoveToTools : ButtonAction
{
	//public Direction direction;
	public Vector3 location;
	public Vector3 destination; 
	public Vector3 placeToMove = new Vector3(500, 71.4f, -154); 
	private int numTrack = 0;
	public float speed;
	private bool mMoving = false;

	void Start()
	{
		location = transform.position;
	}

	void Update () 
	{
		if(mMoving)
		{			
			transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
		}

		// With UI this isn't activating ever.  So I commented out some code below
		if (transform.position == destination)
		{
			mMoving = false;
			DataHolder.toolRackMoving = false;
		}
	}

	public override void takeAction()
	{
		if (gameObject.tag == "Map")
		{
			if (DataHolder.allowInteractions)
			{
				numTrack++;
				if(numTrack % 2 == 1) 
				{
					destination = placeToMove;
				}
				else if(numTrack % 2 == 0)
				{	
					destination = location;
				}

				mMoving=true;
			}
		}
		else
		{
			if (DataHolder.allowInteractions && DataHolder.keysFound == 3)
			{
				numTrack++;
				if(numTrack % 2 == 1) 
				{
					destination = placeToMove;
				}
				else if(numTrack % 2 == 0)
				{	
					destination = location;
				}
				
				mMoving=true;
				//DataHolder.toolRackMoving = true;
			}
		}
	}

	public void MoveToDestination()
	{
		destination = placeToMove;

		mMoving = true;
	}

	public void MoveToOriginalLocation()
	{
		destination = location;

		mMoving = true;
	}
}
