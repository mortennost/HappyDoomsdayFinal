using UnityEngine;
using System.Collections;

[RequireComponent (typeof ( Attack ))]
public class SlowAttack : MonoBehaviour {
	
	public float _baseSlowPercentage = 0.1f;
	public float _baseDuration = 4.0f;
	
	public float _slowPercentageModifier = 1.0f;
	public float _durationModifier = 1.0f;
	
	private float _slowPercentage;
	private float _duration;
	
	public float SlowPercentage
	{
		set { _slowPercentage = value; }
		get { return _slowPercentage; }		
	}
	public float Duration
	{
		set { _duration = value; }
		get { return _duration; }
	}
	// Use this for initialization
	
	void Start()
	{
		UpdateStats();
	}
	
	public void UpdateStats()
	{
		int lvl = gameObject.GetComponent<Level>().GetLevel() - 1;
			
		SlowPercentage = _baseSlowPercentage * Mathf.Pow( _slowPercentageModifier, lvl );
		Duration = _baseDuration * Mathf.Pow( _durationModifier, lvl );
	}
}
