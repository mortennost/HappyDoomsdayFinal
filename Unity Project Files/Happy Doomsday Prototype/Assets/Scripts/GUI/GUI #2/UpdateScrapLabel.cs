using UnityEngine;
using System.Collections;

public class UpdateScrapLabel : MonoBehaviour 
{
	int scrapCount;
	//int maxScrapCount;
		
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		scrapCount = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetScrap();
		//maxScrapCount = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxScrap();
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblScrap.label.text = "  " + scrapCount;
	}
}
