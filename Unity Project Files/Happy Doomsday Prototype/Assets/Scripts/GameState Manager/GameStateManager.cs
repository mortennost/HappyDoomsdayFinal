using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : StateManager
{
	public new void Start()
	{
		SetStack (new Stack<State>());
		
		PushState(new PlayState(gameObject));
	}
}