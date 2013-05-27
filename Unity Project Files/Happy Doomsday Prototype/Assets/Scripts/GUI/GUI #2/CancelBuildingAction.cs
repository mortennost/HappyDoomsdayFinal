using UnityEngine;
using System.Collections;
using iGUI;

public class CancelBuildingAction : iGUIAction {
	

	public override void act(iGUIElement caller)
	{
		Destroy(GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone);
		GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone = null;
		
		if(!GameObject.Find("GameStateManager").GetComponent<GameStateManager>().GetPeek().ToString().Equals("InvasionState"))
		{
			GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new PlayState(GameObject.Find("GameStateManager")));
		}
	}
}
