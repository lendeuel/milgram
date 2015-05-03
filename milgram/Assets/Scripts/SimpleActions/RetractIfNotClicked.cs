using UnityEngine;
using System.Collections;

public class RetractIfNotClicked : MonoBehaviour 
{
	public MoveToTools moveToToRetract;
	public GameObject ignore;
	public GameObject[] objectsToCheck;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log("In RetractIfNotClicked Update.");

		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			//Debug.Log("In RetractIfNotClicked MouseUp.");

			RaycastHit2D hit = new RaycastHit2D();        
			Ray ray = GameObject.FindGameObjectWithTag("MainCamera").camera.ScreenPointToRay (Input.mousePosition);

			//hit = Physics2D.Raycast(Input.mousePosition, Camera.main.ScreenPointToRay(Input.mousePosition).direction, Mathf.Infinity);

			hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if (hit)
			{
				//Debug.Log(hit.collider.gameObject.name);

				if (hit.collider.gameObject != ignore)
				{
					bool retract = true;

					foreach(GameObject g in objectsToCheck)
					{
						if (hit.collider.gameObject == g)
						{
							//Debug.Log("Clicked!!!");

							retract = false;

							break;
						}
						//else
						//{
						//	Debug.Log("Clicked outside");
						//}
					}

					if (retract)
					{
						//Debug.Log("Retracting " + ignore.name);

						Retract();
					}
				}
				//else
				//{
				//	Debug.Log("Hit " + ignore.name);
				//}
			}
			else
			{
				//Debug.Log ("Clicked outside of any object.");

				Retract();
			}
		}
	}

	void Retract()
	{
		moveToToRetract.MoveToOriginalLocation();
	}
}
