using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DisableUserForks : ButtonAction 
{
	private Text option1Text;
	private BoxCollider2D option1Collider;
	private Text option2Text;
	private BoxCollider2D option2Collider;

	void Start()
	{
		option1Text = GameObject.FindGameObjectWithTag("Option1").GetComponent<Text>();
		option2Text = GameObject.FindGameObjectWithTag("Option2").GetComponent<Text>();
		option1Collider = GameObject.FindGameObjectWithTag("Option1").GetComponent<BoxCollider2D>();
		option2Collider = GameObject.FindGameObjectWithTag("Option2").GetComponent<BoxCollider2D>();
	}

	public override void takeAction()
	{
		option1Text.enabled = false;
		option2Text.enabled = false;
		option1Collider.enabled = false;
		option2Collider.enabled = false;
	}
}
