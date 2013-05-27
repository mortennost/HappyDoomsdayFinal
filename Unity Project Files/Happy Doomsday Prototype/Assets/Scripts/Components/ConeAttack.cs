using UnityEngine;
using System.Collections;

public class ConeAttack : MonoBehaviour {

	public float _degree;
	
	private float _cone;
	public float Cone {
		get { return _cone; }
		set { _cone = value; }
	}
	
	// Use this for initialization
	void Start () {
		Cone = Mathf.Cos ( _degree * Mathf.Deg2Rad );
	}
}
