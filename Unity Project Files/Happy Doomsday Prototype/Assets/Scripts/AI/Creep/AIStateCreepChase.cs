using UnityEngine;
using System.Collections.Generic;

public class AIStateCreepChase : State {
	
	private float _searchTimer;
	private Vector3 _oldTargetPosition;
	private bool hasAnim;
	
	public AIStateCreepChase( GameObject gameObject ) : base( gameObject ) {
		
	}
	
	public override void OnStart() {
		
		
		// First check for animator component for nullreference avoidance.
		if ( GetGameObject().GetComponentInChildren<Animator>() ) {
			
			GetGameObject().GetComponentInChildren<Animator>().SetBool( "Walking", true );
		}		
		
		_searchTimer = 0.0f;
		_oldTargetPosition = GetGameObject().GetComponent<Target>().GetTarget().transform.position;
		
	}
	public override void OnPause() {}
	public override void OnContinue() {}
	public override void OnStop()
	{
		// First check for animator component for nullreference avoidance.
		if ( GetGameObject().GetComponentInChildren<Animator>() ) {
			
			GetGameObject().GetComponentInChildren<Animator>().SetBool( "Walking", false );
		}
		
		// remove our path so it can make new calculations next chase state
		GetGameObject().GetComponent<Move>().HasPath = false;
	}
	
	public override void OnExecute() {
		
		//print ( gameObject.tag + " is chasing" );
		
		if ( GetGameObject().GetComponent<Target>().GetTarget() != null )
		{
			
			GameObject curTarget = GetGameObject().GetComponent<Target>().GetTarget();
			if ( _searchTimer >= 1.0f ) {
				// check if our target has moved. if it has, find new path
				if ( Vector3.SqrMagnitude( curTarget.transform.position - _oldTargetPosition ) > 0.1f )
				{
					GetGameObject().GetComponent<Move>().FindPath( curTarget );
				}
				_searchTimer = 0.0f;
			} else {
				_searchTimer += Time.deltaTime;
			}
			
			// Get a path for our creep
			if ( ! GetGameObject().GetComponent<Move>().HasPath )
			{
				//Debug.Log("Searching for path for creep");
				GetGameObject().GetComponent<Move>().FindPath( GetGameObject().GetComponent<Target>().GetTarget() );
				//GetGameObject().GetComponent<Move>()._hasPath = true;
			}
			
			if ( GetGameObject().GetComponent<Attack>().InAttackRange() )
			{				
				GetGameObject().GetComponent<CreepStateManager>().ChangeState( new AIStateCreepAttack( GetGameObject() ) );
				
			}
		} else {
			
			GetGameObject().GetComponent<CreepStateManager>().ChangeState ( new AIStateCreepIdle( GetGameObject() ) );
			
		}
	}
}
