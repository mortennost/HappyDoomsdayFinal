using UnityEngine;
using System.Collections;

public class iTweenTest : MonoBehaviour {
	
	Vector3[] path = new Vector3[4];
	
	// Use this for initialization
	void Start () 
	{
		path[0] = new Vector3(0, 0, 0);
		path[1] = new Vector3(30, 5, 50);
		path[2] = new Vector3(-30, -5, 0);
		path[3] = new Vector3(0, 0, 0);
		
		//iTween.RotateBy(gameObject, iTween.Hash("x", .25, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", .4));
		//iTween.RotateBy(gameObject, iTween.Hash("x", .25, "y", .25, "z", .25, "loopType", "pingPong", "easeType", iTween.EaseType.easeInBounce, "time", 5));
		iTween.MoveTo(gameObject, iTween.Hash(iT.MoveTo.path, path, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", .3));
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
