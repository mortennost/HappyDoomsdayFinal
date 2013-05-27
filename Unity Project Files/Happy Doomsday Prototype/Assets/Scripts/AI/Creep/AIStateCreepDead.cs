using UnityEngine;
using System.Collections;

public class AIStateCreepDead : State 
{

	private float deathTimer;
	private static GameObject _particleManager;
	
	public AIStateCreepDead( GameObject gameObject ) : base( gameObject ) {
		
	}
	
	public override void OnStart() 
	{
		GetGameObject().tag = "Untargetable";
		
		// First check for animator component for nullreference avoidance.
		if ( GetGameObject().GetComponentInChildren<Animator>() ) {
			
			GetGameObject().GetComponentInChildren<Animator>().SetBool( "Dead", true );
		}
		
		_particleManager = GameObject.Find ( "ParticleManager" );
		
		
		deathTimer = 4.0f;
		
	}
	
	public override void OnPause() {}
	public override void OnContinue() {}
	public override void OnStop() {}
	
	public override void OnExecute() 
	{
		
		if ( deathTimer <= 0.0f )
		{
			GameObject.Destroy( GetGameObject() );
			
			// Add XP
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddExperience(GetGameObject().GetComponent<UnitStats>()._experienceGain * GetGameObject().GetComponent<Level>().GetLevel());
			
			// 5% chance to drop scrap
			int random = Random.Range(1, 100);
			if(random <= 10)
			{
				GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddScrap(10);
				_particleManager.GetComponent<ParticleManager>().AddParticle(
					"ScrapPickup",
					GetGameObject().transform.position + Vector3.up,
					Quaternion.identity );
				
			} else if ( random <= 20 )
			{
				GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddFood( 10 );
				_particleManager.GetComponent<ParticleManager>().AddParticle(
					"FoodPickup",
					GetGameObject().transform.position + Vector3.up,
					Quaternion.identity );
				
			} else if ( random <= 20 )
			{
				GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddWater( 10 );
				_particleManager.GetComponent<ParticleManager>().AddParticle(
					"WaterPickup",
					GetGameObject().transform.position + Vector3.up,
					Quaternion.identity );
				
			}
		}
		else 
		{
			deathTimer -= Time.deltaTime;
		}
	}	
}