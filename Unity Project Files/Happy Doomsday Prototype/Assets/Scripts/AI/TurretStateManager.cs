using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof ( Target ))]
[RequireComponent (typeof ( Attack ))]
[RequireComponent (typeof ( Health ))]
public class TurretStateManager : StateManager {
	
	// to skip the first iteration of update so structure state manager can init its stack.
	private bool _skippedFirst = false;
	// Use this for initialization
	public new void Start () {
		SetStack( new Stack<State>() );
		
		PushState(new AIStateTurretIdle( gameObject ) );
	}
	
	// Update is called once per frame
	public new void Update () {
		
		if ( _skippedFirst ) {
			if ( GetComponent<StructureStateManager>().GetPeek().ToString() == "AIStateStructureOperational" ) {
				GetStack().Peek().OnExecute();
			} else {
				//print ( GetComponent<StructureStateManager>().GetPeek().ToString() );
			}
		} else {
			_skippedFirst = true;
			//print ( GetComponent<StructureStateManager>().GetPeek().ToString() );
		}
	}
}