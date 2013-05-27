using UnityEngine;
using System.Collections;

public class UpdateFoodLabel : MonoBehaviour 
{
	int foodCount;
	int maxFoodCount;
		
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		foodCount = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetFood();
		maxFoodCount = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxFood();
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblFood.label.text = "  " + foodCount + " / " + maxFoodCount;
	}
}
