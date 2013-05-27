using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof ( Target ))]
public class Attack : MonoBehaviour {
	
	public string _attackParticle;
	public string _attackImpactParticle;
	
	public float _baseAttackRange = 5;
	public int _baseAttackDamage = 5;
	public float _baseAttackSpeed = 1;
	
	public float _attackRangeModifier = 1.0f;
	public float _attackDamageModifier = 1.0f;
	public float _attackSpeedModifier = 1.0f;
	
	private float _attackRange;
	private int _attackDamage;
	private float _attackSpeed;
	
	public float AttackRange {
		get { return _attackRange; }
	}
	public int AttackDamage {
		get { return _attackDamage; }
	}
	public float AttackSpeed {
		set
		{
			_attackSpeed = value;
			_nextAttack = Time.realtimeSinceStartup + _attackSpeed;
		}
		get { return _attackSpeed; }
	}
	
	private float _nextAttack;
	
	private static GameObject _particleManager;
	
	// Use this for initialization
	void Start () {
		
		_particleManager = GameObject.Find ( "ParticleManager" );
		_nextAttack = 0;
		UpdateStats();
	}
	
	public void UpdateStats() {
		
		int lvl = gameObject.GetComponent<Level>().GetLevel() - 1;
		_attackRange = _baseAttackRange * Mathf.Pow( _attackRangeModifier, lvl );
		_attackDamage = (int)( _baseAttackDamage * Mathf.Pow( _attackDamageModifier, lvl ) );
		_attackSpeed = _baseAttackSpeed * Mathf.Pow ( _attackSpeedModifier, lvl );
		
	}
	
	// checks if attack delay is present or not.
	public bool AttackReady() {
		
		if ( _nextAttack <= Time.realtimeSinceStartup )
		{
			_nextAttack = AttackSpeed + Time.realtimeSinceStartup;
			//print ( gameObject.tag + "Attack ready" );
			return true;
		}
		//print ( gameObject.tag + " Attack Not ready" );
		return false;
	}
	
	// check if target is in attack range.
	public bool InAttackRange() {
		
		GameObject target = GetComponent<Target>().GetTarget();
		
		if ( target == null )
		{
			//print ( gameObject.tag + " Error: no target" );
			return false;
		}
		
		float distance = Vector3.Distance( transform.position, target.transform.position );
		
		// check if target is a structure;
		if ( target.GetComponent<StructureScript>() )
		{
			// if it is then we need to calculate its size by using its lowest value axis as a radius.
			int tempX = target.GetComponent<StructureScript>().xSize;
			int tempZ = target.GetComponent<StructureScript>().zSize;
			
			float radius = tempX <= tempZ ? tempX/1.5f : tempZ/1.5f;
			
			distance -= radius;
			
		}
		
		// using sqrMagnitude since its faster than taking the squareroot, then Pow(x, 2 ) on rest.
		if ( distance <= AttackRange )
			return true;
		
		//print ( gameObject.tag + " not in " + _attackRange + " m range" );
		return false;
	}
	
	
	// deals damage to target and checks for AOE, Slow and Crit
	// bool in case we have to check if it actually happened.
	public void DealDamage() 
	{

		if(GetComponent<PrefabSoundEffects>().audioSources[0])
		{
			GetComponent<PrefabSoundEffects>().audioSources[0].Play();
		}
		
		GameObject target = GetComponent<Target>().GetTarget ();
		
		if ( target != null )
		{
			//ArrayList<GameObject> targets = new ArrayList<GameObject>();
			//GameObject[] targets = new GameObject[100];
			List<GameObject> targets = new List<GameObject>();
			
			float damage = AttackDamage;
			
			if ( GetComponent<AOEAttack>() )
			{
				GameObject[] tempTargets = GameObject.FindGameObjectsWithTag( target.tag );
				
				float radius = GetComponent<AOEAttack>().Radius;
				//targets = new GameObject[tempTargets.Length];
				foreach ( GameObject t in tempTargets )
				{
					// calculate distance between all the found objects and our primary target
					float tempDist = Vector3.Distance ( target.transform.position, t.transform.position );
					// if within distance put it in our array;
					if ( tempDist <= radius )
					{
						targets.Add( t );
					}
					//print( "found " + targets. + " targets" );
				}
			} else if ( GetComponent<AOEAroundSelfAttack>() )
			{
				GameObject[] tempTargets = GameObject.FindGameObjectsWithTag( target.tag );
				
				float radius = GetComponent<AOEAroundSelfAttack>().Radius;
				foreach ( GameObject t in tempTargets )
					
				{
					float tempDist = Vector3.Distance ( transform.position, t.transform.position );
					if ( tempDist <= radius )
					{
						targets.Add ( t );
					}
					
				}
				
			} else if (GetComponent<ConeAttack>() )
			{
				GameObject[] tempTargets = GameObject.FindGameObjectsWithTag( target.tag );
				
				float tempDist;
				float radius = GetComponent<Attack>().AttackRange;
				
				Vector3 heading;
				float dot;
				float cone = GetComponent<ConeAttack>().Cone;
								
				foreach ( GameObject t in tempTargets )
				{
					// check the range first. this eliminates in the cheapest way.
					tempDist = Vector3.Distance ( transform.position, t.transform.position );
					
					if ( tempDist <= radius )
					{
						// now we calculate the heading to target;
						heading = (t.transform.position - transform.position).normalized;
						// get the dot product since this will be the COS of the ANGLE between the two vectors.
						dot = Vector3.Dot ( transform.forward, heading );
						if ( dot > cone )
						{
							targets.Add( t );
						}
					}
				}
			} else {
				// theres no AOE attack, so only our primary target to put in our targets array :>
				targets.Add( target );
				//targets[0] = target;
			}
			
			
			// Now iterate trough our targets array and deal damage;
			
			bool oneTimeParticleCastForAOEAttacksInsanePig = false;
			foreach ( GameObject t in targets )
			{
				// check if this attack slows
				if ( GetComponent<SlowAttack>() )
				{
					// this adds slow effect or resets the effects timer.
					
					t.GetComponent<StatusEffectManager>().AddEffect(
						new MovementEffect(
							t.gameObject,
							GetComponent<SlowAttack>().Duration,
							GetComponent<SlowAttack>().SlowPercentage ) );
					
					
					//print ( "checked for slow effect" );
				}
				
				// check for mindcontrol component and put the mind control effect on the target.
				if ( GetComponent<MindControlAttack>() )
				{
					t.GetComponent<StatusEffectManager>().AddEffect(
						new MindControlEffect(
							t,
							5,
							"Enemy" ) );
				}
				
				// check for taunt component, and put taunt effect on the target.
				if ( GetComponent<TauntAttack>() )
				{
					t.GetComponent<StatusEffectManager>().AddEffect(
						new TauntEffect(
							t,
							GetComponent<TauntAttack>().TauntDuration,
							gameObject ) );
				}
				
				// check for AttackSpeedAttack component to put attackspeed debuff on target.
				if ( GetComponent<AttackSpeedAttack>() )
				{
					t.GetComponent<StatusEffectManager>().AddEffect(
						new AttackSpeedEffect(
							t,
							GetComponent<AttackSpeedAttack>().Duration,
							GetComponent<AttackSpeedAttack>().AttackSpeedDebuffMultiplier ) );
					
				}
				
				// check for criticalattack component and if this attack is a critical hit
				if ( GetComponent<CriticalAttack>() && GetComponent<CriticalAttack>().Crit() )
				{
					damage *= GetComponent<CriticalAttack>().CritMultiplier;
					//print ( "it crit" );
				}
				
				// Check for Reflect component on the target. and check if a reflect did occur
				if ( t.GetComponent<Reflect>() && t.GetComponent<Reflect>().Reflects() )
				{
					// a reflect did occur so remove attackers health based on ReflectDamage
					gameObject.GetComponent<Health>().RemoveHealth( t.GetComponent<Reflect>().ReflectDamage );
				}
				
				if ( ! _attackParticle.Equals( string.Empty ) )
				{
					if ( GetComponent<AOEAroundSelfAttack>() )
					{
						if (!oneTimeParticleCastForAOEAttacksInsanePig) 
						{
							_particleManager.GetComponent<ParticleManager>().AddParticle(
								_attackParticle,
								(Vector3.up * 0.5f) + transform.position,
								Quaternion.identity );
							
							oneTimeParticleCastForAOEAttacksInsanePig = true;
						}

					} else if ( GetComponent<ConeAttack>() )
					{
						if (!oneTimeParticleCastForAOEAttacksInsanePig)
						{
							_particleManager.GetComponent<ParticleManager>().AddParticle(
								_attackParticle,
								Vector3.up + transform.position + transform.forward,
								Quaternion.LookRotation( transform.forward ) );
							
							oneTimeParticleCastForAOEAttacksInsanePig = true;
						}
					}
					else
					{
						_particleManager.GetComponent<ParticleManager>().AddParticle(
							_attackParticle,
							Vector3.up + transform.position + transform.forward,
							Quaternion.LookRotation( transform.forward ) );
					}
					
				}
				
				if ( ! _attackImpactParticle.Equals( string.Empty) )
				{
					_particleManager.GetComponent<ParticleManager>().AddParticle(
						_attackImpactParticle,
						t.transform.position + Vector3.up,
						Quaternion.identity );
				}
				
				t.GetComponent<Health>().RemoveHealth( (int)damage );				
			}
		}
	}
}
