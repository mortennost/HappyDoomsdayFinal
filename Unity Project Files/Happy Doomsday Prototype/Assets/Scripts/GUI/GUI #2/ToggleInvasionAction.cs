using UnityEngine;
using System.Collections;
using iGUI;

public class ToggleInvasionAction : iGUIAction 
{
	
	//bool invasion;
	
	// Use this for initialization
	void Start () 
	{
		//invasion = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public override void act (iGUIElement caller)
	{
		/*
		if(!invasion)
		{
			invasion = true;
			GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new InvasionState(GameObject.Find("GameStateManager")));
		}
		else
		{
			invasion = false;
			GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new PlayState(GameObject.Find("GameStateManager")));
		}
		*/
		
		GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new PlayState(GameObject.Find("GameStateManager")));
	}
}
