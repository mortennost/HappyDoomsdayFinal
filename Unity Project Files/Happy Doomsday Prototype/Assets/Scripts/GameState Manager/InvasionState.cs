using UnityEngine;
using System.Collections;

public class InvasionState : State
{
	public InvasionState(GameObject gameObject)
		: base(gameObject)
	{
	}
	
	public override void OnStart()
	{
		Debug.Log(GameObject.Find("GameStateManager").GetComponent<GameStateManager>().GetPeek().ToString());
		
		GameObject.Find("Input Manager").GetComponent<InputHandler>().SwitchToPlayInputLayer();
		
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotification.GetComponent<NotificationScript>().ShowMessage("Invasion Started");
	}
	
	public override void OnPause()
	{		
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotification.GetComponent<NotificationScript>().ShowMessage("Invasion Paused");
	}
	
	public override void OnExecute()
	{
	}
	
	public override void OnContinue()
	{
		Debug.Log(GameObject.Find("GameStateManager").GetComponent<GameStateManager>().GetPeek().ToString());
		
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotification.GetComponent<NotificationScript>().ShowMessage("Invasion Continued");
	}
	
	public override void OnStop()
	{
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotification.GetComponent<NotificationScript>().ShowMessage("Invasion Ended");
	}
}

