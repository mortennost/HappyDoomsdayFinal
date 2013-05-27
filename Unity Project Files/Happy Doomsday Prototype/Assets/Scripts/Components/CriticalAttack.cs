using UnityEngine;
using System.Collections;

[RequireComponent (typeof ( Attack ))]
public class CriticalAttack : MonoBehaviour {
	
	public float _baseCritChance = 0.10f;	
	public float _critChanceModifier = 1.0f;
	
	private float _critChance;
	private float _critMultiplier = 1.5f;
	
	public float CritChance
	{
		get { return _critChance; }
		set { _critChance = value; }
	}
	public float CritMultiplier
	{
		get { return _critMultiplier; }
		set { _critMultiplier = value; }
	}
	
	// Use this for initialization
	void Start () {
		
		UpdateStats();
		
	}
	
	public void UpdateStats()
	{
		int lvl = gameObject.GetComponent<Level>().GetLevel() - 1;
		
		CritChance = _baseCritChance * Mathf.Pow ( _critChanceModifier, lvl );
	}
	
	/* return true if its a critical hit.*/
	public bool Crit() {
		
		if ( Random.value <= CritChance )
		{
			return true;
		}
		return false;
	}
}
