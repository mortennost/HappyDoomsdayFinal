using UnityEngine;
using System.Collections;
using iGUI;

public class PopStateAction : iGUIAction 
{
	public override void act (iGUIElement caller)
	{
		GameObject.Find("GameStateManager").GetComponent<GameStateManager>().PopState();
	}
}
