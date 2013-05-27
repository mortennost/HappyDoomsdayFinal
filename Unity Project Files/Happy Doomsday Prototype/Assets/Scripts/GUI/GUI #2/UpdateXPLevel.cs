using UnityEngine;
using System.Collections;

public class UpdateXPLevel : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblXPLevel.label.text = "" + GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetLevel();
	}
}
