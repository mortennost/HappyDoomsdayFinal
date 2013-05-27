using UnityEngine;
using System.Collections;

public class AIStateTurretAttack : State {
	
	public AIStateTurretAttack( GameObject gameObject ) : base( gameObject ) {
		
	}	
	
	public override void OnStart()
	{
		// First check for animator component for nullreference avoidance.
		// then set the speed of the animation.
		if ( GetGameObject().GetComponentInChildren<Animator>() ) {
					
			GetGameObject().GetComponentInChildren<Animator>().speed = 4 / GetGameObject().GetComponent<Attack>().AttackSpeed;
		}
	}
	
	public override void OnPause() {}
	public override void OnContinue() {}
	public override void OnStop()
	{
		// First check for animator component for nullreference avoidance.
		// then set attacking to false.
		if ( GetGameObject().GetComponentInChildren<Animator>() ) {
			
			GetGameObject().GetComponentInChildren<Animator>().SetBool( "Attacking", false );
		}
		
	}
	
	public override void OnExecute() {
		
		
		// we dont want this to change target ie: go back to idle if it allready has a target;
		
		if ( GetGameObject().GetComponent<Target>().GetTarget() != null &&
			GetGameObject().GetComponent<Target>().GetTarget().GetComponent<Health>().getHealth() > 0) 
		{
			// check if the target is in range and if attack is ready;
			if (GetGameObject().GetComponent<Attack>().InAttackRange() &&
				GetGameObject().GetComponent<Attack>().AttackReady() ) 
			{
				// attack
				
				// First check for animator component for nullreference avoidance.
				if ( GetGameObject().GetComponentInChildren<Animator>() )
				{
					GetGameObject().GetComponentInChildren<Animator>().SetBool( "Attacking", false );
					GetGameObject().GetComponentInChildren<Animator>().SetBool( "Attacking", true );
				}
				
				// set look at target here, since it allways go here between getting new targets.
				GetGameObject().GetComponent<Target>().lookAtTarget();
				
				GetGameObject().GetComponent<Attack>().DealDamage();
			}
			else
			{
				if ( ! GetGameObject().GetComponent<Attack>().InAttackRange() )
				{
					
					GetGameObject().GetComponent<Target>().SetTarget( null );
						
					GetGameObject().GetComponent<TurretStateManager>().ChangeState ( new AIStateTurretIdle( GetGameObject() ) );
				}
			}
			
		}
		else
		{
			GetGameObject().GetComponent<TurretStateManager>().ChangeState ( new AIStateTurretIdle( GetGameObject() ) );
		}
	}	
}
