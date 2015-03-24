using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour 
{
	public int index;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void LoadLevel(int levelIndexToLoad)
	{
		Application.LoadLevel(levelIndexToLoad);
	}
}
