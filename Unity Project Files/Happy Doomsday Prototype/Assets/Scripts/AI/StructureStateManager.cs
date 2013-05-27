using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof ( Health ))]
[RequireComponent (typeof ( StructureScript ))]
public class StructureStateManager : StateManager {

	// Use this for initialization
	public new void Start () {
		SetStack( new Stack<State>() );
		
		PushState(new AIStateStructurePlacement( gameObject ) );
	}
	
	public new void Update()
	{
		
		if ( ( 	gameObject.GetComponent<Health>().getHealth() <= 0 ) &&
				GetPeek().ToString().Equals( "AIStateStructureOperational" ) )
		{
			PrefabSoundEffects sounds = GetComponent<PrefabSoundEffects>();
			if (sounds != null)
				sounds.audioSources[1].Play();
			ChangeState( new AIStateStructureDestroyed( gameObject ) );
		}
		
		GetPeek().OnExecute();
	}
	
	public void FixedUpdate()
	{
		if ( ! GetPeek().ToString().Equals ( "AIStateStructurePlacement" ) &&
			 ! GetPeek().ToString().Equals ( "AIStateStructureDestroyed" ) )
		{
			CheckSurroundingCreeps();
		}
		
	}
	
	private void CheckSurroundingCreeps()
	{
		
		GameObject[] tempTargets = GameObject.FindGameObjectsWithTag( "Enemy" );
		
		float radius = 	GetComponent<StructureScript>().xSize <= GetComponent<StructureScript>().zSize ?
						GetComponent<StructureScript>().xSize/2 : GetComponent<StructureScript>().zSize/2;
		
		List<GameObject> needsPushing = new List<GameObject>();
		
		foreach ( GameObject t in tempTargets )
		{
			float tempDist = Vector3.SqrMagnitude( t.transform.position - gameObject.transform.position );
			// calculate distance between all the found objects and our primary target
			//float tempDist = Vector3.Distance ( target.transform.position, t.transform.position );
			// if within distance put it in our array;
			if ( tempDist <= radius )
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
