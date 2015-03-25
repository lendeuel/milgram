using UnityEngine;
using System.Collections;

public class DropTool : MonoBehaviour 
{
	public GameObject toolRack;

	public bool isMouseUp;
	public bool dropped;
	public bool collidingWithDrawer;

	Collision lastCollisionEnter;
	
	TypeWriter tw;
	ToolLocations tc;

	int hammerDialogueCount = 1;

	void Start()
	{
		isMouseUp = true;
		dropped = false;

		tw = FindObjectOfType<TypeWriter> ();
		tc = FindObjectOfType<ToolLocations> ();

		lastCollisionEnter = new Collision();
	}

	void Update()
	{
		if (isMouseUp && dropped && collidingWithDrawer)
		{
			//Debug.Log("Dropped in Drawer");
			FindObjectOfType<OpenFile>().fileToOpen.SetActive(false); // Close file if it's open

			dropped = false;
			collidingWithDrawer = false;
			ToolInteractions (lastCollisionEnter);
		}

		//Debug.Log("MouseUp: " + isMouseUp + " Dropped: " + dropped + " CollidingWithDrawer: " + collidingWithDrawer);
	}

	void OnCollisionEnter(Collision col) 
	{
		//Debug.Log("DropTool. OnCollisionEnter. Tag: " + col.collider.tag);

		collidingWithDrawer = true;

		lastCollisionEnter = col;
	}

	void ToolInteractions(Collision col)
	{
		// Reset Tool
		//col.gameObject.renderer.enabled = false;
		//col.gameObject.transform.parent = toolRack.transform; 

		// For some odd reason.  This line of code was causing the monobehaviour on the tools to misbehave.  I rewrote
		//    this commented out code below under Reset Tool 2
		//col.gameObject.transform.position = new Vector3(toolRack.transform.position.x - Random.Range(2,6),transform.position.y + 5, -21);  

		//col.gameObject.renderer.enabled = true;

		// Reset Tool 2
		tc.Move(col.gameObject);

		// Play a Dialogue if needed
		if (col.collider.tag == "Hammer")
		{
			if (hammerDialogueCount > 0)
			{
				tw.QueueMessage("NOOOOOOO! NOT THE HAMMER!", 1);
				tw.QueueMessage("OWWW!!!", 1);
				hammerDialogueCount--;
			}
		}

		// Modify Stats
		col.gameObject.GetComponent<ModifyStats>().modify();
	}
}
