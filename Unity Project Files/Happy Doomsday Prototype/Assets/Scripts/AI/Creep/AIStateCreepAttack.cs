using UnityEngine;
using System.Collections;

public class AIStateCreepAttack : State {
	
	private bool hasAnim;
	
	public AIStateCreepAttack( GameObject gameObject ) : base( gameObject ) {
		
	}
	
	public override void OnStart() {
		// First check for animator component for nullreference avoidance.
		if ( GetGameObject().GetComponentInChildren<Animator>() ) {
		
			GetGameObject().GetComponentInChildren<Animator>().SetBool( "Attacking", true );
			GetGameObject().GetComponentInChildren<Animator>().speed = 1 / GetGameObject().GetComponent<Attack>().AttackSpeed;
		}
	}
	public override void OnPause() {}
	public override void OnContinue() {}
	public override void OnStop()
	{
		// First check for animator component for nullreference avoidance.
		if ( GetGameObject().GetComponentInChildren<Animator>() ) {
			GetGameObject().GetComponentInChildren<Animator>().SetBool( "Attacking", false );
		}

	}
	
	public override void OnExecute() {
		
		if ( GetGameObject().GetComponent<Target>().GetTarget() != null &&
			 GetGameObject().GetComponent<Target>().GetTarget().GetComponent<Health>().CurHealth > 0 )
		{
			
			Attack attackComp = GetGameObject().GetComponent<Attack>();
		
			if ( attackComp.InAttackRange() )
			{
				if ( attackComp.AttackReady() )
				{
					// set look at target here, since it allways go here between getting new targets.
					GetGameObject().GetComponent<Target>().lookAtTarget();
					// deal some epic dmg
					attackComp.DealDamage();
				}
			} else {
	
				GetGameObject().GetComponent<CreepStateManager>().ChangeState( new AIStateCreepChase( GetGameObject() ) );
			}
		} else {
				
			GetGameObject().GetComponent<CreepStateManager>().ChangeState( new AIStateCreepIdle( GetGameObject() ) );
		}
	}	
}