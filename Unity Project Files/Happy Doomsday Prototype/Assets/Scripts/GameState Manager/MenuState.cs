using UnityEngine;
using System.Collections;

public class MenuState : State
{
	public MenuState(GameObject gameObject)
		: base(gameObject)
	{
	}
	
	public override void OnStart()
	{
		Debug.Log(GameObject.Find("GameStateManager").GetComponent<GameStateManager>().GetPeek().ToString());
		
		GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
		
		foreach(GameObject structure in structures)
		{
			// Deactivate TargetRadius-indicator
			if(structure.transform.FindChild("TargetRadius") != null)
			{
				structure.transform.FindChild("TargetRadius").gameObject.SetActive(false);
			}
		}
		
		// Switch to MenuInputLayer
		GameObject.Find("Input Manager").GetComponent<InputHandler>().SwitchToMenuInputLayer();
		
		// Release camera target
		Camera.mainCamera.GetComponent<CameraScript>().target = null;
	}
	
	public override void OnPause()
	{
	}
	
	public override void OnExecute()
	{
	}
	
	public override void OnContinue()
	{
		Debug.Log(GameObject.Find("GameStateManager").GetComponent<GameStateManager>().GetPeek().ToString());
		
		GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
		
		foreach(GameObject structure in structures)
		{
			// Deactivate TargetRadius-indicator
			if(structure.transform.FindChild("TargetRadius") != null)
			{
				structure.transform.FindChild("TargetRadius").gameObject.SetActive(false);
			}
		}
		
		// Switch to MenuInputLayer
		GameObject.Find("Input Manager").GetComponent<InputHandler>().SwitchToMenuInputLayer();
		
		// Release camera target
		Camera.mainCamera.GetComponent<CameraScript>().target = null;
	}
	
	public override void OnStop()
	{
	}
}

