using UnityEngine;
using System.Collections;
using iGUI;

public class AddExperienceAction : iGUIAction 
{
	private const float XP_INCREMENT_PERCENTAGE = 0.15f;
	
	public override void act (iGUIElement caller)
	{
		GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddExperience(
			(int) Mathf.Abs(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxExperience() * XP_INCREMENT_PERCENTAGE));
	}
}
