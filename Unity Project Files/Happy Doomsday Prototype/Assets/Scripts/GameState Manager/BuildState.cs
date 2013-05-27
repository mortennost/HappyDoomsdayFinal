using UnityEngine;
using System.Collections;

public class BuildState : State
{
	public BuildState(GameObject gameObject)
		: base(gameObject)
	{
	}
	
	public override void OnStart()
	{
		// Switch to BuildInputLayer
		GameObject.Find("Input Manager").GetComponent<InputHandler>().SwitchToBuildInputLayer();
		
		Debug.Log(GameObject.Find("GameStateManager").GetComponent<GameStateManager>().GetPeek().ToString());
		
		// Set the Gridsnapper to center of Grid
		//GameObject.Find("Gridsnapper").transform.position = GameObject.Find("Grid").GetComponent<GridScript>().GetCenter();
		
		// Set camera target to the gridsnapper
		Ray currentRay = Camera.mainCamera.ScreenPointToRay(new Vector3(Screen.width /2.0f, Screen.height /2.0f, 0.0f));
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		float currentEnter = 0.0f;
		plane.Raycast(currentRay, out currentEnter);
		Vector3 newGridsnapperPos = currentRay.origin + currentEnter * currentRay.direction;
		GameObject.Find("Gridsnapper").transform.position = newGridsnapperPos;
		
		Camera.mainCamera.GetComponent<CameraScript>().target = GameObject.Find("Gridsnapper");
		
		GameObject.Find("ConstructionHighlight Z").renderer.enabled = true;
		GameObject.Find("ConstructionHighlight X").renderer.enabled = true;
		
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOpenShop.passive = true;
	}
	
	public override void OnPause()
	{
		GameObject.Find("ConstructionHighlight Z").renderer.enabled = false;
		GameObject.Find("ConstructionHighlight X").renderer.enabled = false;
		
		// Release camera target
		Camera.mainCamera.GetComponent<CameraScript>().target = null;
	}
	
	public override void OnExecute()
	{
	}
	
	public override void OnContinue()
	{
		Debug.Log(GameObject.Find("GameStateManager").GetComponent<GameStateManager>().GetPeek().ToString());
		
		GameObject.Find("ConstructionHighlight Z").renderer.enabled = true;
		GameObject.Find("ConstructionHighlight X").renderer.enabled = true;
		
		// Switch to BuildInputLayer
		GameObject.Find("Input Manager").GetComponent<InputHandler>().SwitchToBuildInputLayer();
		
		// Set camera target to the gridsnapper
		Camera.mainCamera.GetComponent<CameraScript>().target = GameObject.Find("Gridsnapper");
	}
	
	public override void OnStop()
	{
		GameObject.Find("ConstructionHighlight Z").renderer.enabled = false;
		GameObject.Find("ConstructionHighlight X").renderer.enabled = false;
		
		// Release camera target
		Camera.mainCamera.GetComponent<CameraScript>().target = null;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOpenShop.passive = false;
	}
}

