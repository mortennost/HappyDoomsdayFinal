using UnityEngine;
using System.Collections.Generic;

public class StatusEffectManager : MonoBehaviour {
	
	private List<StatusEffect> _list;
	private List<int> _removeList;

	// Use this for initialization
	void Start () {
		_list = new List<StatusEffect>();
		_removeList = new List<int>();
	}
	
	// Adds an effect to the gameobject.
	public void AddEffect( StatusEffect effect )
	{
		// check if the effect allready is in the list. if true: call its resetTimer.
		bool isInList = false;
		foreach ( StatusEffect e in _list )
		{
			if ( e.ToString() == effect.ToString() ) {
				isInList = true;
				e.ResetTimer();;
			}
		}
		
		if ( !isInList ) {
			_list.Add( effect );
			_list[ _list.Count - 1 ].OnStart();
		}
	}
	
	void Update()
	{
		
		
		
		
		// first check if we have any effects in place;
		if ( _list.Count > 0 )
		{
			// now update the timer on all effects. and if they return false they are completed.
			foreach( StatusEffect e in _list )
			{
				if ( ! e.UpdateTimer() )
				{
					// the effect has run out of time. Stop it, and remove it from the list;
					e.OnStop();
					_removeList.Add( _list.IndexOf(e) );
				}
			}
			
			if ( _removeList.Count > 0 )
			{
				for ( int i = ( _removeList.Count - 1 ); i >= 0; --i )
				{
					_list.RemoveAt( _removeList[i] );
				}
				
				_removeList.Clear ();
			}
			
			
			/*
			if ( _removeList.Count > 0 )
			{
				foreach( int i in _removeList ) {
					_list.RemoveAt( i );
				}
				_removeList.Clear ();
			}*/
		}
		
	}
}
