using UnityEngine;
using System.Collections;

public class UpdateWorkersLabel : MonoBehaviour 
{
	int workersCount;
	int maxWorkersCount;
		
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{	
		workersCount = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWorkerCount();
		maxWorkersCount = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxWorkerCount();
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblWorkers.label.text = "  " + workersCount + " / " + maxWorkersCount;
	}
}
