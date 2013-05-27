using UnityEngine;
using System.Collections;
using iGUI;

public class AddResourcesAction : iGUIAction 
{
	public override void act (iGUIElement caller)
	{
		GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddFood(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxFood());
		GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddWater(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxWater());
	}
}
