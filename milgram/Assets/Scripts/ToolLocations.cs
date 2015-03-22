using UnityEngine;
using System.Collections;

public class ToolLocations : MonoBehaviour 
{
	public GameObject[] tools;

	Vector3[] toolStartLocations;

	// Use this for initialization
	void Start () 
	{
		int count = 0;

		toolStartLocations = new Vector3[tools.Length];

		foreach(GameObject g in tools)
		{
			toolStartLocations[count] = g.transform.position;

			count++;
		}
	}

	public void Move(GameObject tool)
	{
		for (int i = 0; i < tools.Length; i++)
		{
			if (tools[i].tag == tool.tag)
			{
				tool.transform.position = toolStartLocations[i];
			}
		}
	}
}
