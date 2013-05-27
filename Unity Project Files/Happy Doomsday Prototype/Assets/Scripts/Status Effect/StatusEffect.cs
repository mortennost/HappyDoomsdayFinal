using UnityEngine;
using System.Collections;

public abstract class StatusEffect {
	
	private float _duration;
	private float _timer;
	private GameObject _gameObject;
	
	public StatusEffect( GameObject gameObject, float duration )
	{
		SetGameObject( gameObject );
		_duration = duration;
		// set timer to duration.
		ResetTimer();
	}
	
	public GameObject GetGameObject()
	{
		return _gameObject;	
	}
	
	private void SetGameObject(GameObject gameObject)
	{
		_gameObject = gameObject;	
	}
	// Use this for initialization
	public abstract void OnStart();
	
	// tick the timer. This returns false when the timer <= 0 and true otherwise.
	public bool UpdateTimer()
	{
		_timer -= Time.fixedDeltaTime;
		
		if ( _timer <= 0 )
		{
			// return false when the status effect is done
			return false;
		}
		// return true as long as the StatusEffect its not done
		return true;
		
	}
	
	public abstract void OnStop();
	
	public void ResetTimer()
	{
		_timer = _duration;
	}
}
