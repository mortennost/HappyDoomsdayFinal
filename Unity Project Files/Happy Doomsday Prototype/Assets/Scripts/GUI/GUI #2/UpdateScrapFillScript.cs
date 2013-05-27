using UnityEngine;
using System.Collections;

public class UpdateScrapFillScript : MonoBehaviour 
{
	//float maxWidth = 95.0f;
	
	//int currentScrap;
	//int maxScrap;
	
	// Use this for initialization
	void Start () 
	{
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgScrapFill.setWidth(95.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		currentScrap = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetScrap();
		maxScrap = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxScrap();
		
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgScrapFill.setWidth(((float)currentScrap / (float)maxScrap) * maxWidth);
		*/
	}
}
