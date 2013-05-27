using UnityEngine;
using System.Collections;
using iGUI;

public class HarvestAction : iGUIAction {
	
	[HideInInspector]
	public GameObject obj;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void act (iGUIElement caller)
	{
		Harvest(obj);
		Camera.mainCamera.GetComponent<CameraScript>().target = null;
	}
	
	public void Harvest(GameObject obj)
	{
		// Deplete water/food -> Update resources
		if(obj.GetComponent<WaterGatherer>() != null)
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddWater(obj.GetComponent<WaterGatherer>().DepleteWater());
		}
		else if(obj.GetComponent<FoodGatherer>() != null)
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddFood(obj.GetComponent<FoodGatherer>().DepleteFood());
		}
		
		//obj.transform.localScale = GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().normalScale;
		GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().Scale(obj, GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().normalScale);
	}
}
