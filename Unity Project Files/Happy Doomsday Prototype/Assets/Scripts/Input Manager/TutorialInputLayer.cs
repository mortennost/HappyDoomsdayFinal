using UnityEngine;
using System.Collections;

public class TutorialInputLayer : InputLayer {
	
	InputHandler inputHandler;

	// Use this for initialization
	void Start () {
		
		inputHandler = gameObject.GetComponent<InputHandler>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			inputHandler.SwitchToBuildInputLayer();
		}
		
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			inputHandler.SwitchToMenuInputLayer();
		}
	}
}
