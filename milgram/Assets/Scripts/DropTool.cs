using UnityEngine;
using System.Collections;

public class DropTool : MonoBehaviour 
{
	public GameObject toolRack;

	Collision lastCollision;

	TypeWriter tw;

	void Start()
	{
		tw = FindObjectOfType<TypeWriter> ();

		lastCollision = new Collision ();
	}

	void OnCollisionEnter(Collision col) 
	{
		lastCollision = col;

		if (col.collider.tag == "Tool") 
		{
			//Play Sound
			//Start Text Interaction with Harvey
			//Unenable Tool

			ResetTool(col);

			Debug.Log("I'm in the Collision for Tool in DropTool");
		}
		else if (col.collider.tag == "Hammer")
		{
			ResetTool(col);

			Debug.Log("I'm in the Collision for Hammer in DropTool");
		}

	}

	void ResetTool(Collision col)
	{
		col.gameObject.SetActive(false); 
		col.gameObject.transform.parent = toolRack.transform; 
		col.gameObject.transform.position = new Vector3(toolRack.transform.position.x - Random.Range(2,6),transform.position.y + 5, -21);
		col.gameObject.SetActive(true);
	}
}
