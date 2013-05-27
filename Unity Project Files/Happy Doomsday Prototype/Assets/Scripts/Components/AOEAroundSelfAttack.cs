using UnityEngine;
using System.Collections;

[RequireComponent (typeof ( Attack ))]
[RequireComponent (typeof ( Level ))]
public class AOEAroundSelfAttack : MonoBehaviour {
	
	
	public float _baseRadius;
	public float _radiusModifier;
	
	private float _radius;
	
	public float Radius {
		get { return _radius; }
		set { _radius = value; }
	}
	
	// Use this for initialization
	void Start () {
		UpdateStats();
	}
	
	public void UpdateStats() {
		
		int lvl = GetComponent<Level>().GetLevel() - 1;
		Radius = _baseRadius * Mathf.Pow ( _radiusModifier, lvl );
	}
}
