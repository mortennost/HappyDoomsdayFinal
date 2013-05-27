using UnityEngine;
using System.Collections;

[RequireComponent (typeof ( Attack ))]
public class AttackSpeedAttack : MonoBehaviour {

	public float _baseAttackSpeedDebuffMultiplier = 0.1f;
	public float _baseDuration = 4.0f;
	
	public float _attackSpeedDebuffMultiplierModifier = 1.0f;
	public float _durationModifier = 1.0f;
	
	private float _attackSpeedDebuffMultiplier;
	private float _duration;
	
	public float AttackSpeedDebuffMultiplier
	{
		set { _attackSpeedDebuffMultiplier = value; }
		get { return _attackSpeedDebuffMultiplier; }		
	}
	public float Duration
	{
		set { _duration = value; }
		get { return _duration; }
	}
	// Use this for initialization
	
	void Start()
	{
		UpdateStats();
	}
	
	public void UpdateStats()
	{
		int lvl = gameObject.GetComponent<Level>().GetLevel() - 1;
			
		AttackSpeedDebuffMultiplier = _baseAttackSpeedDebuffMultiplier * Mathf.Pow( _attackSpeedDebuffMultiplierModifier, lvl );
		Duration = _baseDuration * Mathf.Pow ( _durationModifier, lvl );
		
	}
}
