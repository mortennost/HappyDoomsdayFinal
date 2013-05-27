using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof ( Target ))]
[RequireComponent (typeof ( Move ))]
[RequireComponent (typeof ( Attack ))]
[RequireComponent (typeof ( Health ))]
[RequireComponent (typeof ( StatusEffectManager ))]
public class CreepStateManager : StateManager {

	// Use this for initialization
	public new void Start () {
		SetStack( new Stack<State>() );
		
		PushState(new AIStateCreepIdle( gameObject ) );
	}
	
	public new void Update()
	{
		
		if ( gameObject.GetComponent<Health>().getHealth() <= 0 &&
			 !GetPeek().ToString().Equals( "AIStateCreepDead" ) )
		{
			ChangeState( new AIStateCreepDead( gameObject ) );
		}
		
		GetPeek().OnExecute();
	}
	
	public void FixedUpdate()
	{
		
		CheckSurroundingCreeps();
		
	}
	
	private void CheckSurroundingCreeps()
	{
		
		GameObject[] tempTargets = GameObject.FindGameObjectsWithTag( "Enemy" );
		
		List<GameObject> needsPushing = new List<GameObject>();
		
		foreach ( GameObject t in tempTargets )
		{
			float tempDist = Vector3.SqrMagnitude( t.transform.position - gameObject.transform.position );
			// calculate distance between all the found objects and our primary target
			//float tempDist = Vector3.Distance ( target.transform.position, t.transform.position );
			// if within distance put it in our array;
			if ( tempDist <= 0.8f )
			{
				needsPushing.Add( t );
			}
			//print( "found " + targets. + " targets" );
		}
		
		if ( needsPushing.Count > 0 )
		{
			
			foreach( GameObject p in needsPushing )
			{
				
				// get unit vector
				Vector3 tempUnit = Vector3.Normalize( p.transform.position - gameObject.transform.position );
				p.GetComponent<Move>().AddForce( tempUnit );
				
			}
			
		}
	}
	
}
