using UnityEngine;
using System.Collections;

public class DropTool : MonoBehaviour {

	public GameObject toolRack;

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Tool" ) {
			//Play Sound
			//Start Text Interaction with Harvey
			//Unenable Tool

			col.gameObject.SetActive(false); 
			col.gameObject.transform.parent = toolRack.transform; 
			col.gameObject.transform.position = new Vector3(toolRack.transform.position.x - Random.Range(2,6),transform.position.y + 5, -21);
			col.gameObject.SetActive(true);
		}
	}
}
