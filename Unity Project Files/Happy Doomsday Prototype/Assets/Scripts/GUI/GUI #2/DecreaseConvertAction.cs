using UnityEngine;
using System.Collections;
using iGUI;

public class DecreaseConvertAction : iGUIAction 
{
	int foodConvert;
	int waterConvert;
	int scrapCostConvert;
	
	//int currentFood;
	//int currentWater;
	//int currentScrap;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public override void act (iGUIElement caller)
	{
		// Get the current convert values
		foodConvert = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._foodConvert;
		waterConvert = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._waterConvert;
		scrapCostConvert = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._scrapCostConvert;
		
		// Get the current resources
		//currentFood = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetFood();
		//currentWater = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWater();
		//currentScrap = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetScrap();
		
		// Update labels if we have enough scrap
		if(scrapCostConvert > 0)
		{
			foodConvert -= 10;
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._foodConvert = foodConvert;
			waterConvert -= 10;
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._waterConvert = waterConvert;
			scrapCostConvert -= 20;
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._scrapCostConvert = scrapCostConvert;
			
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblFoodWaterConvert.label.text = foodConvert + "/" + waterConvert;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblScrapCostConvert.label.text = "" + scrapCostConvert;
		}
	}
}
