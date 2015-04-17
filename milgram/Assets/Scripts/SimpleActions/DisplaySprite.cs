using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplaySprite : MonoBehaviour 
{
	public Sprite[] theSprites;
	public int currentIndex;

	public void Display(int index)
	{
		currentIndex = index;
		gameObject.GetComponent<Image>().sprite = theSprites[index];
	}
}
