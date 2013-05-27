using UnityEngine;
using System.Collections;

public class TauntEffect : StatusEffect {
	
	GameObject _newTarget;
	
	public TauntEffect( GameObject gameObject, float duration, GameObject newTarget ) : base( gameObject, duration )
	{
		_newTarget = newTarget;
	}
	// Use this for initialization
	public override void OnStart()
	{
		
		GetGameObject().GetComponent<Target>().SetTarget( _newTarget );
	}
	
	// Update is called once per frame
	public override void OnStop()
	{
		GetGameObject().GetComponent<Target>().SetTarget( null );
		
	}
}
