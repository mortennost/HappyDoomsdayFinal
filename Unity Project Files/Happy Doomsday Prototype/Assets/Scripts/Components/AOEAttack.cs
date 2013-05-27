using UnityEngine;
using System.Collections;

[RequireComponent (typeof ( Attack ))]
public class AOEAttack : MonoBehaviour {
	
	public float _radius;
	
	public float Radius {
		get { return _radius; }
		set { _radius = value; }
	}
}
