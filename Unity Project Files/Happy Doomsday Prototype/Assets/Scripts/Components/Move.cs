using UnityEngine;
using System.Collections.Generic;
using AssemblyCSharp;

public class Move : MonoBehaviour {
	
	//private Vector3 _direction;
	public float _movementSpeed = 1;
	private bool _hasPath;
	
	public bool HasPath {
		set { _hasPath = value; }
		get { return _hasPath; }
	}
	
	private List<Vector3> _movementPath;
	private int _currentStep;
	private float _margin = 0.5f;
	private GridScript _gridScript;
	//private int _endNode;
	private float _durrTimer;
	private float _nextStepTimer;
	
	private Vector3 _finalDirection;
	
	public float MovementSpeed
	{
		get { return _movementSpeed; }
		set { _movementSpeed = value; }
	}
	
	// Use this for initialization
	void Start () {
		
		_durrTimer = 0.0f;
		_gridScript = GameObject.Find( "Grid" ).GetComponent<GridScript>();
		
		_currentStep = 0;
		_hasPath = false;
		
		_nextStepTimer = 2.0f;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
		//print ("Current movementspeed: " + MovementSpeed );
		if ( HasPath ) {
			
			
			for ( int i = 0; i < _movementPath.Count - 1; ++i ) {
				Debug.DrawLine( _movementPath[i], _movementPath[ i + 1 ], Color.red );
			}
			
			// check if we have done our step. and increment it if we have :>
			if ( (Vector3.SqrMagnitude(_movementPath[ _currentStep ] - transform.position ) <= _margin ) &&
				 ( _currentStep < _movementPath.Count - 1 ) )
			{
				
				++_currentStep;
				//Debug.Log( "step is: " + _currentStep );
				_finalDirection = Vector3.Normalize( _movementPath[ _currentStep ] - transform.position );
				_nextStepTimer = 2.0f;
			}
			
			if ( _nextStepTimer <= 0 && _currentStep < _movementPath.Count - 1 )
			{
				++_currentStep;
				_finalDirection = Vector3.Normalize( _movementPath[ _currentStep ] - transform.position );
				_nextStepTimer = 2.0f;
			} else {
				_nextStepTimer -= Time.fixedDeltaTime;
			}
			
			updateHeading();
			updatePosition();
			
			if ( _currentStep == _movementPath.Count - 1 )
			{
				if ( Vector3.SqrMagnitude( _movementPath[ _currentStep ] - transform.position ) >= 3.0f )
					HasPath = false;
			}
			
		} else {
			
			
			
		}
	}
	
	public void AddForce( Vector3 force )
	{
		transform.Translate( force * Time.fixedDeltaTime,Space.World );
		//_finalDirection = Vector3.Normalize( _finalDirection + force );
	}
	
	private void updateHeading()
	{
		
		//transform.forward = Vector3.Normalize( _finalDirection ); 
		
		
		if ( transform.forward != _finalDirection ) {
			//print( "updating transform.forward" );
			_durrTimer += 0.02f;
			if ( _durrTimer > 1.0f )
				_durrTimer = 1.0f;
			
			transform.forward = Vector3.Normalize( Vector3.Lerp( transform.forward,
								_finalDirection,
								_durrTimer ) );
			
			
		} else {
			//print ( "it should be right now o0" );
			_durrTimer = 0;
		}
		
	}
	
	public void FindPath( GameObject target ) {
		
		// have to find which node of the target is the shortest path;
		// first find a unit vector giving us the direction from the target to our creep
		
		Vector3 position = target.transform.position;
		if ( target.tag == "Structure" || target.tag == "Playerhouse"  )
		{
		
			Vector3 unit = Vector3.Normalize( transform.position - target.transform.position );
			
			// now we have to find wich ratio we will multiply x and z of the unit vector with for
			// odd shapes.
			float xRatio = target.GetComponent<StructureScript>().xSize / 2;
			float zRatio = target.GetComponent<StructureScript>().zSize / 2;
			
			unit.x *= xRatio;
			unit.z *= zRatio;
			
			// now we have distorted the unit vector and we need to add this vector to the targets
			// position to get the final position of our node.
			
			position += unit;
		}
		
		
		_movementPath = _gridScript.DirGraph.GetShortestPath( transform.position, position );
		
		if ( _movementPath.Count > 1 ) {
			
			HasPath = true;
			_currentStep = 0;
			
			//_endNode = _gridScript.DirGraph.GetClosestVertex( _movementPath[ _movementPath.Count - 1 ] );
			transform.forward = Vector3.Normalize( _movementPath[ _currentStep ] - transform.position );
			
		} else {
			HasPath = false;
			GetComponent<Target>().SetTarget( null );
			GetComponent<StatusEffectManager>().AddEffect( new WallKillerEffect( gameObject, 5 ) );
		}
	}
	
	// translate according to direction vector.
	public void updatePosition() { 
		
		transform.Translate( Vector3.forward * _movementSpeed * Time.fixedDeltaTime );
		
	}
}
