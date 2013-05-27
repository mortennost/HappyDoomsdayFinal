using UnityEngine;
using System.Collections;

public class UpdateXPFill : MonoBehaviour 
{
	int currentExperience;
	int maxExperience;
	
	//float width;
	
	// Use this for initialization
	void Start () 
	{
		//width = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgXPFillColor.rect.width;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentExperience = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetExperience();
		maxExperience = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxExperience();
		//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgXPFillColor.scale = new Vector2(((float)currentExperience / (float)maxExperience), 1.0f);
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgXPFillColor.setWidth((float)currentExperience / (float)maxExperience);
	}
}
