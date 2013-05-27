using UnityEngine;
using System.Collections;

public class UpdateInvasionLabelScript : MonoBehaviour 
{	
	float _nextInvasion;
	string _suffix;
	
	bool messageShown;
	
	// Use this for initialization
	void Start () 
	{
		_suffix = "";
		messageShown = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		_nextInvasion = GameObject.Find("GameManager").GetComponent<GameManagerScript>().timeUntilNextInvasion / 60.0f;
		
		if(_nextInvasion < 1.0f)
		{
			_nextInvasion *= 60;
			_suffix = "s";
			
			if(!messageShown)
			{
				GameObject.Find("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("The invading robots will \nreach us in 1 minute! \nGet ready!");
				messageShown = true;
			}
		}
		else
		{
			_suffix = "m";
			messageShown = false;
		}
		
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblInvasionTimer.label.text = "" + Mathf.Ceil(_nextInvasion) + _suffix;
	}
}
