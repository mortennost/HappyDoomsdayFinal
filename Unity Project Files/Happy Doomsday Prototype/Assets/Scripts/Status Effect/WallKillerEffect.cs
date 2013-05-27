using UnityEngine;
using System.Collections;

public class WallKillerEffect : StatusEffect {
	
	private string _priorityTarget;
	private string _initialPriorityTarget;
	
	public WallKillerEffect( GameObject gameObject, float duration ) : base( gameObject, duration )
	{
		_priorityTarget = "Wall";
		
	}
	
	public override void OnStart () {
		_initialPriorityTarget = GetGameObject().GetComponent<Target>().PriorityTarget;
		
		//GetGameObject().GetComponent<Target>().SetTarget( null );
		GetGameObject().GetComponent<Target>().PriorityTarget = _priorityTarget;
	}
		
	public override void OnStop()
	{
		//GetGameObject().GetComponent<Target>().SetTarget( null );
		GetGameObject().GetComponent<Target>().PriorityTarget = _initialPriorityTarget;
	}
}
