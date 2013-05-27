using UnityEngine;
using System.Collections;

public abstract class State
{
	private GameObject gameObject;
	
	public State(GameObject gameObject)
	{
		SetGameObject(gameObject);	
	}
	
	public GameObject GetGameObject()
	{
		return this.gameObject;	
	}
	
	private void SetGameObject(GameObject gameObject)
	{
		this.gameObject = gameObject;
	}
	
	public abstract void OnStart();
	public abstract void OnPause();
	public abstract void OnExecute();
	public abstract void OnContinue();
	public abstract void OnStop();
}
