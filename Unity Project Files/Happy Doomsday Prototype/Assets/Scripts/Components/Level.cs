using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	
	private int _level = 1;
	private static GameObject _particleManager;
	// Use this for initialization
	
	void Start()
	{
		_particleManager = GameObject.Find ( "ParticleManager" );
	}
	
	public void IncreaseLevel()
	{
		if ( _level < 50 )
			_level += 1;
		else
			_level = 50;
		
		
		_particleManager.GetComponent<ParticleManager>().AddParticle(
			"LevelUp",
			transform.position + Vector3.up,
			Quaternion.identity );
			
		UpdateComponents();
	}
	public void DecreaseLevel()
	{
		_level -= 1;
		UpdateComponents();
	}
	
	public int GetLevel() { return _level; }
	public void SetLevel( int val )
	{
		_level = val;
		UpdateComponents();
	}
	
	public void UpdateComponents()
	{
		
		if ( GetComponent<Health>() )
			GetComponent<Health>().UpdateStats();
		
		if ( GetComponent<StructureScript>() )
			GetComponent<StructureScript>().UpdateStats();
		
		if ( GetComponent<Attack>() )
			GetComponent<Attack>().UpdateStats();
		
		if ( GetComponent<CriticalAttack>() )
			GetComponent<CriticalAttack>().UpdateStats();
		if ( GetComponent<SlowAttack>() )
			GetComponent<SlowAttack>().UpdateStats();
		if ( GetComponent<TauntAttack>() )
			GetComponent<TauntAttack>().UpdateStats();
		if ( GetComponent<AOEAroundSelfAttack>() )
			GetComponent<AOEAroundSelfAttack>().UpdateStats();
		//if ( GetComponent<AOEAttack>() )
		//	GetComponent<AOEAttack>().UpdateStats();
		
		if ( GetComponent<Reflect>() )
			GetComponent<Reflect>().UpdateStats();
		
		if ( GetComponent<FoodGatherer>() )
			GetComponent<FoodGatherer>().UpdateStats();
		
		if ( GetComponent<WaterGatherer>() )
			GetComponent<WaterGatherer>().UpdateStats();
		
		
	}
}
