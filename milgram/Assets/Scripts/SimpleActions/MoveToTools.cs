using UnityEngine;
using System.Collections;
using System;

public class MoveToTools : ButtonAction
{
	//public Direction direction;
	public Vector3 location;
	public Vector3 destination; 
	public Vector3 placeToMove = new Vector3(500, 71.4f, -154); 
	private int numTrack = 0;
	public float speed;
	public bool mMoving = false;

	[NonSerialized]public bool movedOut = false;

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
	}

	public override void takeAction()
	{
		//if (gameObject.tag == "Map")
		//{
			if (DataHolder.allowInteractions)
			{
				numTrack++;
				if(numTrack % 2 == 1) 
				{
					movedOut = true;
					destination = placeToMove;
				}
				else if(numTrack % 2 == 0)
				{	
					movedOut = false;
					destination = location;
				}

				mMoving=true;
			}
//		}
//		else
//		{
//			if (DataHolder.allowInteractions)// && DataHolder.keysFound == 3)
//			{
//				numTrack++;
//				if(numTrack % 2 == 1) 
//				{
//					movedOut = true;
//					destination = placeToMove;
//				}
//				else if(numTrack % 2 == 0)
//				{	
//					movedOut = false;
//					destination = location;
//				}
//				
//				mMoving=true;
//			}
//		}
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
