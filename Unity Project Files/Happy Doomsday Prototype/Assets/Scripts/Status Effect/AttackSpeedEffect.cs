using UnityEngine;
using System.Collections;

public class AttackSpeedEffect : StatusEffect {
	
	private float _attackSpeedMultiplier;
	private float _initialAttackSpeed;
	
	public AttackSpeedEffect( GameObject go, float duration, float attackSpeedMultiplier )
		: base ( go, duration )
	{
		
		_attackSpeedMultiplier = attackSpeedMultiplier;
	}
	
	public override void OnStart()
	{
		// store the initial attack speed
		_initialAttackSpeed = GetGameObject().GetComponent<Attack>().AttackSpeed;
		
		// set new attack speed
		GetGameObject().GetComponent<Attack>().AttackSpeed *= _attackSpeedMultiplier;
	}
		
	public override void OnStop()
	{
		// set attackspeed back to the initial value
		GetGameObject().GetComponent<Attack>().AttackSpeed = _initialAttackSpeed;
	}
}
