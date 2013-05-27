using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public GameObject _target;
	public string _targetType = null;
	public string _priorityTarget = null;
	
	public string TargetType {
		set { _targetType = value; }
		get { return _targetType; }
	}
	
	public string PriorityTarget {
		set { _priorityTarget = value; }
		get { return _priorityTarget; }
	}
	// Use this for initialization
	void Start () {
		_target = null;
		
		//print ( "this objects tag is: " + gameObject.tag );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public GameObject GetTarget() { return _target; }
	public void SetTarget( GameObject t ) { _target = t; }
	
	public GameObject FindNearestTarget(){ 
		
		
		GameObject[] gobjects = GameObject.FindGameObjectsWithTag( TargetType );
		
		GameObject nearestObject = null;
		
		float currentMinDist = Mathf.Infinity;
		
		foreach( GameObject o in gobjects )
		{
			float distance = Vector3.Distance ( transform.position, o.transform.position );
			
			if ( o.GetComponent<StructureScript>() )
			{
				
				if ( o.GetComponent<StructureScript>().structureType == "Wall" )
				{
					distance += distance/4;
				}
				if ( o.GetComponent<StructureScript>().structureType == _priorityTarget )
				{
					distance -= distance/4;
				}
			}
			
			//Debug.Log ("Distance: " + distance + " CurrMinDist: " + currentMinDist);

			if ( distance < currentMinDist )
			{
				
				if ( distance != 0 ) {
					nearestObject = o;
					currentMinDist = distance;
				}
			}
		}		
		
		return nearestObject;
	}
	
	public void lookAtTarget() 
	{
		if(gameObject.transform.FindChild("Model") != null)
			gameObject.transform.LookAt( _target.transform.position );	
	}
}
