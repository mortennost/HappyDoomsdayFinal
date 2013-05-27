using UnityEngine;
using System.Collections;

public class UpdateFoodFillScript : MonoBehaviour 
{
	float maxWidth = 165.0f;
	
	int currentFood;
	int maxFood;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentFood = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetFood();
		maxFood = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxFood();
		
		if((((float)currentFood / (float)maxFood) * maxWidth) < 5.0f)
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgFoodFill.setWidth(5.0f);
		}
		else
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgFoodFill.setWidth(((float)currentFood / (float)maxFood) * maxWidth);
		}
	}
}
