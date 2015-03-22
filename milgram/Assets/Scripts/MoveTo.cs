using UnityEngine;
using System.Collections;


public enum Direction
{
	up=0,down,right,left
};

public class MoveTo : ButtonAction
{
	//public Direction direction;
	public Vector3 location;
	private Vector3 destination; 
	private Vector3 placeToMove = new Vector3 (29,4,-20); 
	private int numTrack = 0;
	public float speed;
	private bool mMoving = false;

	void Start () 
	{
	
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
		numTrack++;
		if(numTrack % 2 == 1) {
			destination = location;
		}
		else if(numTrack % 2 == 0){
			destination = placeToMove;
		}
		mMoving=true;
	}
}
