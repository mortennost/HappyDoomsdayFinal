using UnityEngine;
using System.Collections;
using iGUI;

public class ResetScrapAction : iGUIAction 
{
	public override void act (iGUIElement caller)
	{
		GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().ResetScrap();
	}
}
