using UnityEngine;
using System.Collections;
using iGUI;

public class DestroyStructureAction : iGUIAction {
	
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
		// Give back half food and water/up to resource caps
		if((GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetFood() + obj.GetComponent<StructureScript>().FoodCost / 2) > GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxFood())
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddFood(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxFood() - GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetFood());
		}
		else
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddFood(obj.GetComponent<StructureScript>().FoodCost / 2);
		}
		
		if((GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWater() + obj.GetComponent<StructureScript>().WaterCost / 2) > GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxWater())
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddWater(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxWater() - GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWater());
		}
		else
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddWater(obj.GetComponent<StructureScript>().WaterCost / 2);
		}
		
		if(obj.GetComponent<StructureStateManager>().GetPeek().ToString().Equals("AIStateStructureCreation"))
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().RemoveWorker();
		}
		
		if(obj.GetComponent<StructureScript>().structureType.Equals("Harvester"))
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._currentHarvesters -= 1;
		}
		if(obj.GetComponent<StructureScript>().structureType.Equals("Turret"))
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._currentTurrets -= 1;
		}
		
		// Report this shit to datamining
#if UNITY_WEBPLAYER
		//StartCoroutine( GameObject.Find("DatabaseManager").GetComponent<DatabaseController>().PostDataminingBuilding( "Destruction", obj.name ) );
#endif
		// Set the graphs vertices traversable attribute to false
		StructureScript structureScript = obj.GetComponent<StructureScript>();
		Vector3 cornerPosition = obj.transform.position;
		cornerPosition.x -= structureScript.xSize / 2.0f;
		cornerPosition.z -= structureScript.zSize / 2.0f;
		GameObject.Find("Grid").GetComponent<GridScript>().DirGraph.ToggleTraversable(cornerPosition, structureScript.xSize, structureScript.zSize, true);
		
		Destroy(obj); //Camera.mainCamera.GetComponent<CameraScript>().target
		Camera.mainCamera.GetComponent<CameraScript>().target = null;
	}
}
