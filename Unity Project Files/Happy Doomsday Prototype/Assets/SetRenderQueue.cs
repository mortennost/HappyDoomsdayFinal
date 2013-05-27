using UnityEngine;
using System.Collections;

public class SetRenderQueue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		renderer.material.renderQueue = Mathf.RoundToInt(3000.0f - (transform.parent.position.x + 2.0f));
	}
}
