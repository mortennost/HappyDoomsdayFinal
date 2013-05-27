using UnityEngine;
using System.Collections;

public class AIStateStructurePlacement : State {
	
	//private
	
	

	public AIStateStructurePlacement( GameObject gameObject ) : base( gameObject ) {
		
	}	
	
	public override void OnStart()
	{		
		GetGameObject().tag = "Untargetable";
		
		// gotta make some debrie
		//GetGameObject().GetComponentInChildren<MeshFilter>().mesh = "Plane018";
		
		
	}
	public override void OnPause() {}
	public override void OnContinue() {}
	public override void OnStop()
	{
		//GetGameObject().tag = "Structure";
	}
	
	public override void OnExecute() {
		
		
	}	
}
