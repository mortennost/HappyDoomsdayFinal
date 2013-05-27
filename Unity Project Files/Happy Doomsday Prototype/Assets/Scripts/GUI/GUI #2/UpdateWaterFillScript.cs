using UnityEngine;
using System.Collections;

public class UpdateWaterFillScript : MonoBehaviour 
{
	float maxWidth = 165.0f;
	
	int currentWater;
	int maxWater;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentWater = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWater();
		maxWater = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxWater();
		
		if((((float)currentWater / (float)maxWater) * maxWidth) < 5.0f)
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgWaterFill.setWidth(5.0f);
		}
		else
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgWaterFill.setWidth(((float)currentWater / (float)maxWater) * maxWidth);
		}
	}
}
