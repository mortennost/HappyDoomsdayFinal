using UnityEngine;
using System.Collections;


public class Reflect : MonoBehaviour {
	
	// The damage the reflect does to the attacker
	private int _reflectDamage;
	public int _baseReflectDamage = 1;	
	public float _reflectDamageModifier = 1.0f;
	
	// The chance that we will reflect damage
	private float _reflectChance;
	public float _baseReflectChance;
	public float _reflectChanceModifier = 1.0f;
	
	public int ReflectDamage
	{
		set { _reflectDamage = value; }
		get { return _reflectDamage; }
	}
	
	public float ReflectChance
	{
		set { _reflectChance = value; }
		get { return _reflectChance; }
	}
	
	// Use this for initialization
	void Start ()
	{
		UpdateStats();
	}
		
	public void UpdateStats()
	{
		int lvl = gameObject.GetComponent<Level>().GetLevel() - 1;
		
		ReflectDamage = (int) (_baseReflectDamage * Mathf.Pow ( _reflectDamageModifier, lvl ) );
		ReflectChance = _baseReflectChance * Mathf.Pow ( _reflectChanceModifier, lvl );
	}
	
	// returns true if damage should be reflected.
	public bool Reflects()
	{
		if ( Random.value <= ReflectChance )
			return true;
		else
			return false;
	}
}
