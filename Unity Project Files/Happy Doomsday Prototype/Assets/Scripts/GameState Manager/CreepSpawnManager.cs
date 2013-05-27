using UnityEngine;
using System.Collections;

public class CreepSpawnManager : MonoBehaviour {
	
	public float _spawnDelay = 2;
	public Transform _creep;
	
	private float _nextSpawn;
	
	// Use this for initialization
	void Start () {
		_nextSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( _nextSpawn <= Time.realtimeSinceStartup ) {
			_nextSpawn = Time.realtimeSinceStartup + _spawnDelay;
			
			//Instantiate( _creep, Vector3(5, 0, 5 ), Quaternion.identity );
			
			Vector3 pos = transform.position;
				
			Instantiate( _creep, pos, Quaternion.identity );
		}
	}
}
