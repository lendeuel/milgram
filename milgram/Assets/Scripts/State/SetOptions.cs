using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetOptions : ButtonAction 
{
	public bool censor;
	public float musicVolume;
	public float sfxVolume;

	void OnEnable()
	{
		GameObject.FindGameObjectWithTag("ProfanityToggle").GetComponent<Toggle>().isOn = DataHolder.censorText;
		GameObject.FindGameObjectWithTag("MusicsSlider").GetComponent<Slider>().normalizedValue = DataHolder.musicVolume;
		GameObject.FindGameObjectWithTag("SFXsSlider").GetComponent<Slider>().normalizedValue = DataHolder.sfxVolume;
	}

	// Variations that allow the use of the UI slider or UI toggle
	public void SetMusicVolume(Slider theSlider)
	{
		musicVolume = theSlider.normalizedValue;
	}
	
	public void SetSFXVolume(Slider theSlider)
	{
		sfxVolume = theSlider.normalizedValue;
	}
	
	public void SetCensor(Toggle theToggle)
	{
		censor = theToggle.isOn;
	}
	
	// Variations that allow the use of a value
	public void SetMusicVolume(float value)
	{
		musicVolume = value;
	}
	
	public void SetSFXVolume(float value)
	{
		sfxVolume = value;
	}
	
	public void SetCensor(bool value)
	{
		censor = value;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log("Censor: " + DataHolder.censorText + " Music Volume: " + DataHolder.musicVolume + " SFX Volume: " + DataHolder.sfxVolume);
	}

	public override void takeAction()
	{
		DataHolder.censorText = censor;
		DataHolder.musicVolume = musicVolume;
		DataHolder.sfxVolume = sfxVolume;
	}
}
