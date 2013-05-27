using UnityEngine;
using System.Collections;
using iGUI;

public class ToggleFPSAction : iGUIAction 
{
	#pragma warning disable 0108
	bool enabled = false;
	#pragma warning restore 0108
	
	float updateInterval = 0.5f;
	float accum = 0.0f; 
	int frames = 0;
	float timeLeft;
	
	// Use this for initialization
	void Start () 
	{
		timeLeft = updateInterval;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(enabled)
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblFPSCounter.enabled = true;
			
			timeLeft -= Time.deltaTime;
			accum += Time.timeScale / Time.deltaTime;
			++frames;
			
			if(timeLeft <= 0.0f)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblFPSCounter.label.text = "FPS: " + accum/frames;
				timeLeft = updateInterval;
				accum = 0.0f;
				frames = 0;
			}
		}
		else
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblFPSCounter.enabled = false;
		}
	}
	
	public override void act (iGUIElement caller)
	{
		if(enabled)
		{
			enabled = false;
		}
		else
		{
			enabled = true;
		}
	}
}
