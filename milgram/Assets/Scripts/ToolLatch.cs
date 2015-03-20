using UnityEngine;
using System.Collections;

public class ToolLatch : MonoBehaviour
{
	//public Direction direction;
	public GameObject parentObject;

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "ToolRack" ) {
			transform.parent = parentObject.transform; 
		}
	}

	void OnCollisionExit(Collision col) {
		Debug.Log("DEPARENTED");
		if (col.collider.tag == "ToolRack" ) {
			transform.parent = null; 
		}
	}
}
