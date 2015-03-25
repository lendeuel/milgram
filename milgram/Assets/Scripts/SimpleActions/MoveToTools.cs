using UnityEngine;
using System.Collections;

public class MoveToTools : ButtonAction
{
	//public Direction direction;
	public Vector3 location;
	private Vector3 destination; 
	private Vector3 placeToMove = new Vector3 (185,25,-12); 
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

		if (DataHolder.allowInteractions)
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
}
