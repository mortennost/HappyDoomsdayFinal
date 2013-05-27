using UnityEngine;
using System.Collections;

public class MindControlEffect : StatusEffect {
	
	private string _targetType;
	private string _initialTargetType;
	
	public MindControlEffect( GameObject gameObject, float duration, string targetType ) : base( gameObject, duration )
	{
		_targetType = targetType;
		
	}
	
	public override void OnStart () {
		_initialTargetType = GetGameObject().GetComponent<Target>().TargetType;
		
		GetGameObject().GetComponent<Target>().SetTarget( null );
		GetGameObject().GetComponent<Target>().TargetType = _targetType;
	}
		
	public override void OnStop()
	{
		GetGameObject().GetComponent<Target>().SetTarget( null );
		GetGameObject().GetComponent<Target>().TargetType = _initialTargetType;
	}
}