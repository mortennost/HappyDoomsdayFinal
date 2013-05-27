using UnityEngine;
using System.Collections;

public class PlayState : State
{
	public PlayState(GameObject gameObject)
		: base(gameObject)
	{
	}
	
	public override void OnStart()
	{
		Debug.Log(GameObject.Find("GameStateManager").GetComponent<GameStateManager>().GetPeek().ToString());
		
		if(Camera.mainCamera.GetComponent<CameraScript>().target == null)
		{
			GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
		
			foreach(GameObject structure in structures)
			{
				// Deactivate TargetRadius-indicator
				if(structure.transform.FindChild("TargetRadius") != null)
				{
					structure.transform.FindChild("TargetRadius").gameObject.SetActive(false);
				}
			}
		}
		
		// Switch to PlayInputLayer
		GameObject.Find("Input Manager").GetComponent<InputHandler>().SwitchToPlayInputLayer();
		
		// Release camera target
		Camera.mainCamera.GetComponent<CameraScript>().target = null;
	}
	
	public override void OnPause()
	{
	}
	
	public override void OnExecute()
	{
		if(Camera.mainCamera.GetComponent<CameraScript>().target == null)
		{
			GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
		
			foreach(GameObject structure in structures)
			{
				// Deactivate TargetRadius-indicator
				if(structure.transform.FindChild("TargetRadius") != null)
				{
					structure.transform.FindChild("TargetRadius").gameObject.SetActive(false);
				}
			}
		}
	}
	
	public override void OnContinue()
	{
		Debug.Log(GameObject.Find("GameStateManager").GetComponent<GameStateManager>().GetPeek().ToString());
		
		if(Camera.mainCamera.GetComponent<CameraScript>().target == null)
		{
			GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
		
			foreach(GameObject structure in structures)
			{
				// Deactivate TargetRadius-indicator
				if(structure.transform.FindChild("TargetRadius") != null)
				{
					structure.transform.FindChild("TargetRadius").gameObject.SetActive(false);
				}
			}
		}
		
		// Switch to PlayInputLayer
		GameObject.Find("Input Manager").GetComponent<InputHandler>().SwitchToPlayInputLayer();
		
		// Release camera target
		Camera.mainCamera.GetComponent<CameraScript>().target = null;
	}
	
	public override void OnStop()
	{
	}
}

