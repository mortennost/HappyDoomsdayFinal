using UnityEngine;
using System.Collections;
using iGUI;

public class AddScrapAction : iGUIAction 
{
	public override void act (iGUIElement caller)
	{
		GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddScrap(100);
	}
}
