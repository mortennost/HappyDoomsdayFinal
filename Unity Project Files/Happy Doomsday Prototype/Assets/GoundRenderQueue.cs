using UnityEngine;
using System.Collections;

public class GoundRenderQueue : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.renderQueue = 0;
	}
}
