using UnityEngine;
using System.Collections;

[RequireComponent (typeof ( Attack ))]
public class TauntAttack : MonoBehaviour {
	
	
	public float _baseTauntDuration;
	public float _tauntDurationModifier;
	
	private float _tauntDuration;
	
	
	public float TauntDuration
	{
		set { _tauntDuration = value; }
		get { return _tauntDuration; }
	}
	
	// Use this for initialization
	void Start () {
		
		UpdateStats();
	}
	
	// Update is called once per frame
	public void UpdateStats ()
	{
		int lvl = GetComponent<Level>().GetLevel() - 1;
		
		TauntDuration = _baseTauntDuration * Mathf.Pow( _tauntDurationModifier, lvl );
	}
}
