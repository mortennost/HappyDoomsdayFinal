using UnityEngine;
using System.Collections;

public class UpdateWaterLabel : MonoBehaviour 
{
	int waterCount;
	int maxWaterCount;
		
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{	
		waterCount = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWater();
		maxWaterCount = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxWater();
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblWater.label.text = "  " + waterCount + " / " + maxWaterCount;
	}
}
