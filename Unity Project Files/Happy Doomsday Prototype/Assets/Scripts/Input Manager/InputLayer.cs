using UnityEngine;
using System.Collections;

public abstract class InputLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Enable() {
		enabled = true;
	}
	
	public void Disable() {
		enabled = false;
	}
}
