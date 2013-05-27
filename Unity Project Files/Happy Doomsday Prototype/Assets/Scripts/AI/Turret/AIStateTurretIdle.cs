using UnityEngine;
using System.Collections;

public class AIStateTurretIdle : State {

	private bool hasAnim;
	public AIStateTurretIdle( GameObject gameObject ) : base( gameObject ) {
		
	}
	
	public override void OnStart() {}
	public override void OnPause() {}
	public override void OnContinue() {}
	public override void OnStop() {}
	
	public override void OnExecute() {
		
		
		
		//print( gameObject.tag + " is looking for target" );
		
		GameObject tempTarget = GetGameObject().GetComponent<Target>().FindNearestTarget();
		if ( tempTarget != null )
		{
			GetGameObject().GetComponent<Target>().SetTarget( tempTarget );
			
			GetGameObject().GetComponent<TurretStateManager>().ChangeState( new AIStateTurretAttack( GetGameObject () ) );
			
		}
	}
}
