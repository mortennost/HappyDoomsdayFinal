using UnityEngine;
using System.Collections;

public class MovementEffect : StatusEffect {
	
	private float _movementModifier;
	private float _initialMovementSpeed;
	
	public MovementEffect( GameObject gameObject, float duration, float movementModifier ) : base( gameObject, duration )
	{
		_movementModifier = movementModifier;
	}
	// Use this for initialization
	
	public override void OnStart()
	{
		// store the initial movement speed
		_initialMovementSpeed = GetGameObject().GetComponent<Move>().MovementSpeed;
		// set the new movementspeed;
		GetGameObject().GetComponent<Move>().MovementSpeed *= ( 1 - _movementModifier );
	}
		
	public override void OnStop()
	{
		// set movementspeed to the initial movement speed;
		GetGameObject().GetComponent<Move>().MovementSpeed = _initialMovementSpeed;
	}
}
