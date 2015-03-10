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
	public float speed;
	private bool mMoving=false;

	void Start () 
	{
	
	}
	
	void Update () 
	{
		if(mMoving)
		{
			transform.position = Vector3.MoveTowards(transform.position, location, speed*Time.deltaTime);
		}
	}

	public override void takeAction()
	{
		Debug.Log ("actioning");
		mMoving=true;
	}
}
