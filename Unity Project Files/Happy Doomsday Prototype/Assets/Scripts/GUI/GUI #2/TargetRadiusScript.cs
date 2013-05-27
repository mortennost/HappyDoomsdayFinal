using UnityEngine;
using System.Collections;

public class TargetRadiusScript : MonoBehaviour 
{
	Vector3 scale;

	// Use this for initialization
	void Start () 
	{
		scale = transform.localScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.localScale = new Vector3(scale.x * (transform.parent.GetComponent<Attack>().AttackRange * 2), 1, scale.z * (transform.parent.GetComponent<Attack>().AttackRange * 2));
	}
}
