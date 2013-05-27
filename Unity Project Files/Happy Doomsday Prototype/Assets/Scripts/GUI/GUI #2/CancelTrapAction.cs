using UnityEngine;
using System.Collections;
using iGUI;

public class CancelTrapAction : iGUIAction {
	

	public override void act(iGUIElement caller)
	{
		Destroy(GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone);
		GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone = null;
		GameObject.Find("5-Traps Panel").GetComponent<TrapInputAction>().instantiated = false;
		
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelTraps.enabled = true;
		
		GameObject.Find("ConstructionHighlight Z").renderer.enabled = false;
		GameObject.Find("ConstructionHighlight X").renderer.enabled = false;
		
		GameObject.Find("Input Manager").GetComponent<InputHandler>().SwitchToPlayInputLayer();
	}
}
