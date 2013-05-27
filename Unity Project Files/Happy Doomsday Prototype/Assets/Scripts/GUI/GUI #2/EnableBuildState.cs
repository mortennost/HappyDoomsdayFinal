using UnityEngine;
using System.Collections;
using iGUI;

public class EnableBuildState : iGUIAction {

	public override void act(iGUIElement caller)
	{
		GameObject.Find("GameStateManager").GetComponent<GameStateManager>().PushState(new BuildState(GameObject.Find("GameStateManager")));
	}
}
