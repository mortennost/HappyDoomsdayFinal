using UnityEngine;
using System.Collections;
using iGUI;

public class SpawnCreepAction : iGUIAction {

	
	public override void act(iGUIElement caller)
	{
		GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new StartupInvasionState(GameObject.Find("GameStateManager")));
		GameObject.Find("GameManager").GetComponent<GameManagerScript>().ResetInvasionTimer();
	}
}
