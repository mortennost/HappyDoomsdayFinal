using UnityEngine;
using System.Collections;

public class UpdateHarvestProgressFillScript : MonoBehaviour 
{
	[HideInInspector]
	public GameObject obj;
	
	float maxWidth = 255.0f;
	
	int currentResource;
	int maxResource;
	
	string resourceType;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(obj != null)
		{
			if(obj.GetComponent<FoodGatherer>() != null)
			{
				currentResource = (int)obj.GetComponent<FoodGatherer>().AccumulatedFood;
				maxResource = obj.GetComponent<FoodGatherer>().MaxFood;
				resourceType = "Food: ";
			}
			else if(obj.GetComponent<WaterGatherer>() != null)
			{
				currentResource = (int)obj.GetComponent<WaterGatherer>().AccumulatedWater;
				maxResource = obj.GetComponent<WaterGatherer>().MaxWater;
				resourceType = "Water: ";
			}
			
			if((((float)currentResource / (float)maxResource) * maxWidth) < 5.0f)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgHarvestProgressFill.setWidth(5.0f);
			}
			else
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgHarvestProgressFill.setWidth(((float)currentResource / (float)maxResource) * maxWidth);
			}
			
			//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgHarvestProgressFill.setWidth( ((float) currentResource / (float)maxResource) * maxWidth );
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblHarvestProgress.label.text = resourceType + currentResource + " / " + maxResource;
		}
	}
}
