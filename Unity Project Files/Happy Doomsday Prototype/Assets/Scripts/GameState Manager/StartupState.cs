using UnityEngine;
using AssemblyCSharp;
using System.Collections;

public class StartupState : State
{
	public StartupState(GameObject gameObject)
		: base(gameObject)
	{
		//GameCenterSingleton gcSingleton = GameCenterSingleton.Instance;
	}
	
	public override void OnStart()
	{
	}
	
	public override void OnPause()
	{
	}
	
	public override void OnExecute()
	{
	}
	
	public override void OnContinue()
	{
	}
	
	public override void OnStop()
	{
	}
}

