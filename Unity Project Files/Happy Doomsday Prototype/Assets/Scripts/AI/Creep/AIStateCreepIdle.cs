using UnityEngine;
using System.Collections;

public class AIStateCreepIdle : State {
	
	private bool hasAnim;
	
	public AIStateCreepIdle( GameObject gameObject ) : base( gameObject ) {
		
	}
	
	public override void OnStart() {
		
	}
	public override void OnPause() {}
	public override void OnContinue() {}
	public override void OnStop() {}
	
	public override void OnExecute() { 
		
		
		//Debug.Log( GetGameObject().tag + " is looking for target" );
		
		GameObject tempTarget = GetGameObject().GetComponent<Target>().FindNearestTarget();
		
		if ( tempTarget == null )
			tempTarget = GameObject.Find ( "Playerhouse" );
		
		if ( tempTarget != null )
		{
			GetGameObject().GetComponent<Target>().SetTarget( tempTarget );
			
			GetGameObject().GetComponent<CreepStateManager>().ChangeState( new AIStateCreepChase( GetGameObject () ) );
			
		}
		else 
		{
			GetGameObject().GetComponent<Move>().HasPath = false;

			
			Debug.Log (GetGameObject().ToString() + " Target: NULL");
			
			/*
			if ( hasAnim )
				GetGameObject().GetComponentInChildren<Animation>().animation.Play( "Idle" );*/
		}
	}
}
