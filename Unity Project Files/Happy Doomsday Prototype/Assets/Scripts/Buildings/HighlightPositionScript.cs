using UnityEngine;
using System.Collections;

public class HighlightPositionScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		gameObject.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 newPos = GameObject.Find("Gridsnapper").transform.position;
		newPos.y = 0.1f;
		transform.position = newPos;
	}
}
