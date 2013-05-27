using UnityEngine;
using System.Collections;
using iGUI;

public class ResetExperienceAction : iGUIAction 
{

	public override void act (iGUIElement caller)
	{
		GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().ResetExperience();
	}
}

