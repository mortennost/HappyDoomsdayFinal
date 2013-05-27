using UnityEngine;
using System.Collections;

public class GatheringManager : MonoBehaviour {

	// Use this for initialization
	
	private float _nextHarvestTime;
	private bool _skippedFirst = false;
	
	private FoodGatherer fg;
	private WaterGatherer wg;
	
	private static GameObject _particleManager;
	
	void Start () {
		_nextHarvestTime = Time.realtimeSinceStartup;
		
		if ( GetComponent<FoodGatherer>() ) {
			fg = GetComponent<FoodGatherer>();
		} else if ( GetComponent<WaterGatherer>() ) {
			wg = GetComponent<WaterGatherer>();
		} else {
			print ( "You have not assigned a water- or food gatherer to this object" );
		}
		
		if ( _particleManager == null )
			_particleManager = GameObject.Find ( "ParticleManager" );
		
	}
	
	void FixedUpdate() {
		
		// skippedFirst is used to let structurestatemanager initialize so we dont get an error when checking the stack.
		if ( _skippedFirst ) {
			if ( GetComponent<StructureStateManager>().GetPeek().ToString() == "AIStateStructureOperational" ) {
				
				// check timer here to o.0 save resources? :>
				if ( _nextHarvestTime <= Time.realtimeSinceStartup ) {
					_nextHarvestTime = Time.realtimeSinceStartup + 1.0f;
				
					if ( fg ) {
						// this is a food gatherer :>
						
						// gather and check if its full 
						if ( !fg.Gather() )
						{
							// its full so make some particle effects;
							_particleManager.GetComponent<ParticleManager>().AddParticle(
								"FoodPickup",
								transform.position + Vector3.up,
								Quaternion.identity );
							
						}
						//print ( "Food: " + fg.AccumulatedFood );
					} else if ( wg ) {
						
						// this is a Water gatherer :>
						if ( !wg.Gather() )
						{
							// its full so make some particle effects;
							_particleManager.GetComponent<ParticleManager>().AddParticle(
								"WaterPickup",
								transform.position + Vector3.up,
								Quaternion.identity );
						}
						
					} else {
						
						print ( "This harvester does not have a water or food component" );
					}
				}
			}
		} else {
			_skippedFirst = true;
			//print ( GetComponent<StructureStateManager>().GetPeek().ToString() );
		}
		
		
	}
}
