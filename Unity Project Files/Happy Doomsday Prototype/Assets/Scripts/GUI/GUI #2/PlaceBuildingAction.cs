using UnityEngine;
using System.Collections;
using iGUI;

public class PlaceBuildingAction : iGUIAction {
	
	public bool replaceState;
	
	bool placeable = true;
	
	public override void act(iGUIElement caller)
	{		
		GameObject structureToBeBuilt = GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone;
		StructureScript toBeBuiltScript = structureToBeBuilt.GetComponent<StructureScript>();
		Vector3 toBeBuiltPosition = GameObject.Find("Gridsnapper").transform.position;
		//int xSize = toBeBuiltScript.xSize;
		//int zSize = toBeBuiltScript.zSize;
		
		float xEMin = float.PositiveInfinity;
		float xEMax = float.NegativeInfinity;
		float zEMin = float.PositiveInfinity;
		float zEMax = float.NegativeInfinity;
		
		float xTMin = float.PositiveInfinity;
		float xTMax = float.NegativeInfinity;
		float zTMin = float.PositiveInfinity;
		float zTMax = float.NegativeInfinity;
		
		//bool isOnExistingBuilding = false;
		
		GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
		
		foreach(GameObject existingStructure in structures)
		{			
			StructureScript existingScript = existingStructure.GetComponent<StructureScript>();
			Vector3 existingPosition = existingStructure.transform.position;
			
			xEMin = existingPosition.x - (float) existingScript.xSize / 2.0f;
			xEMax = existingPosition.x + (float) existingScript.xSize / 2.0f;
			zEMin = existingPosition.z - (float) existingScript.zSize / 2.0f;
			zEMax = existingPosition.z + (float) existingScript.zSize / 2.0f;
			
			xTMin = toBeBuiltPosition.x - (float) toBeBuiltScript.xSize / 2.0f;
			xTMax = toBeBuiltPosition.x + (float) toBeBuiltScript.xSize / 2.0f;
			zTMin = toBeBuiltPosition.z - (float) toBeBuiltScript.zSize / 2.0f;
			zTMax = toBeBuiltPosition.z + (float) toBeBuiltScript.zSize / 2.0f;
			
			if (
				((xTMin >= xEMin && xTMin < xEMax) || (xTMax <= xEMax && xTMax > xEMin)) &&
				((zTMin >= zEMin && zTMin < zEMax) || (zTMax <= zEMax && zTMax > zEMin)) &&
				existingStructure != structureToBeBuilt
				)
			{
				//isOnExistingBuilding = true;
				GameObject.Find ("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Can't build here.");
				return;
			}
		}
		
		//////////////PLAYERHOUSE/////////////////////////////////////
		structures = GameObject.FindGameObjectsWithTag("Playerhouse");
		
		foreach(GameObject existingStructure in structures)
		{			
			StructureScript existingScript = existingStructure.GetComponent<StructureScript>();
			Vector3 existingPosition = existingStructure.transform.position;
			
			xEMin = existingPosition.x - (float) existingScript.xSize / 2.0f;
			xEMax = existingPosition.x + (float) existingScript.xSize / 2.0f;
			zEMin = existingPosition.z - (float) existingScript.zSize / 2.0f;
			zEMax = existingPosition.z + (float) existingScript.zSize / 2.0f;
			
			xTMin = toBeBuiltPosition.x - (float) toBeBuiltScript.xSize / 2.0f;
			xTMax = toBeBuiltPosition.x + (float) toBeBuiltScript.xSize / 2.0f;
			zTMin = toBeBuiltPosition.z - (float) toBeBuiltScript.zSize / 2.0f;
			zTMax = toBeBuiltPosition.z + (float) toBeBuiltScript.zSize / 2.0f;
			
			if (
				((xTMin >= xEMin && xTMin < xEMax) || (xTMax <= xEMax && xTMax > xEMin)) &&
				((zTMin >= zEMin && zTMin < zEMax) || (zTMax <= zEMax && zTMax > zEMin))
				)
			{
				//isOnExistingBuilding = true;
				GameObject.Find ("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Can't build here.");
				return;
			}
		}
		///////////////////////////////////////////////////////////////////////////////
		
		if(replaceState)
		{
			// Find the clone-object
			GameObject instance = GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone;
			
			// Set the graphs vertices traversable attribute to false
			StructureScript structureScript = instance.GetComponent<StructureScript>();
			Vector3 cornerPosition = instance.transform.position;
			cornerPosition.x -= structureScript.xSize / 2.0f;
			cornerPosition.z -= structureScript.zSize / 2.0f;
			GameObject.Find("Grid").GetComponent<GridScript>().DirGraph.ToggleTraversable(cornerPosition, structureScript.xSize, structureScript.zSize, false);
			
			// Set material to the normal (diffuse) material
			//instance.GetComponentInChildren<Renderer>().materials = instance.GetComponent<MaterialScript>().normalMaterials;
			//if(instance.transform.FindChild("ParticleEffect") != null)
			//{
			//	instance.transform.FindChild("ParticleEffect").GetComponentInChildren<Renderer>().materials = instance.transform.FindChild("ParticleEffect").GetComponent<MaterialScript>().normalMaterials;
			//}
			
			// Detach structure-object from clone and change to PlayState
			GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone = null;
			instance.tag = "Structure";
			GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new PlayState(GameObject.Find("GameStateManager")));
		
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.fadeTo(0.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.fadeTo(0.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOpenShop.passive = false;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOptions.passive = false;
		
			StartCoroutine(WaitAndDisable());
		}
		else
		{
			if(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWorkerCount() < GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxWorkerCount())
			{
				// Find the clone-object
				GameObject instance = GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone;
				
				if(instance.GetComponent<StructureScript>().structureType == "Harvester")
				{
					if(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._currentHarvesters < GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxHarvesters)
					{
						placeable = true;
					}
					else
					{
						placeable = false;
						GameObject.Find ("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Can't build structure: \nToo many Harvesters currently in compound.");
					}
				}
				else if(instance.GetComponent<StructureScript>().structureType == "Turret")
				{
					if(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._currentTurrets < GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets)
					{
						placeable = true;
					}
					else
					{
						placeable = false;
						GameObject.Find ("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Can't build structure: \nToo many Turrets currently in compound.");
					}
				}
				
				if(placeable)
				{
					if(instance.GetComponent<StructureScript>().structureType.Equals("Harvester"))
					{
						GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._currentHarvesters++;
					}
					else if(instance.GetComponent<StructureScript>().structureType.Equals("Turret"))
					{
						GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._currentTurrets++;
					}
					
					if(instance.transform.FindChild("TargetRadius") != null)
					{
						instance.transform.FindChild("TargetRadius").gameObject.SetActive(false);
					}
					
					// Set the graphs vertices traversable attribute to false
					StructureScript structureScript = instance.GetComponent<StructureScript>();
					Vector3 cornerPosition = instance.transform.position;
					cornerPosition.x -= structureScript.xSize / 2.0f;
					cornerPosition.z -= structureScript.zSize / 2.0f;
					GameObject.Find("Grid").GetComponent<GridScript>().DirGraph.ToggleTraversable(cornerPosition, structureScript.xSize, structureScript.zSize, false);
					
					// Set material to the normal (diffuse) material
					//instance.GetComponentInChildren<Renderer>().materials = instance.GetComponent<MaterialScript>().normalMaterials;
					//if(instance.transform.FindChild("ParticleEffect") != null)
					//{
					//	instance.transform.FindChild("ParticleEffect").GetComponentInChildren<Renderer>().materials = instance.transform.FindChild("ParticleEffect").GetComponent<MaterialScript>().normalMaterials;
					//}
					
					// Detach structure-object from clone and change to PlayState
					GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone = null;
					GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new PlayState(GameObject.Find("GameStateManager")));
					
					instance.GetComponent<StructureStateManager>().ChangeState(new AIStateStructureCreation(instance));
					
					// Deplete resources - Cost set in ResourceManager
					GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().DepleteFood(instance.GetComponent<StructureScript>().FoodCost);
					GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().DepleteWater(instance.GetComponent<StructureScript>().WaterCost);
					GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().DepleteScrap(instance.GetComponent<StructureScript>().ScrapMetalCost);
					
					// Add entry to datamining building table
#if UNITY_WEBPLAYER
					//StartCoroutine( GameObject.Find("DatabaseManager").GetComponent<DatabaseController>().PostDataminingBuilding( "Creation", instance.name ) );
#endif
					
					// Add XP
					GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddExperience(instance.GetComponent<StructureScript>()._experienceGain);
								
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.fadeTo(0.0f, 0.2f);
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.fadeTo(0.0f, 0.2f);
					
					GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetCompoundLevel();
					
					StartCoroutine(WaitAndDisable());
				}
			}
			else
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.fadeTo(0.0f, 0.2f);
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.fadeTo(0.0f, 0.2f);
				
				Destroy(GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone);
				GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone = null;
				GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new PlayState(GameObject.Find("GameStateManager")));
				GameObject.Find ("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Can't build structure: \nNot enough workers.");
				
				StartCoroutine(WaitAndDisable());
			}
		}
	}
	
	IEnumerator WaitAndDisable()
	{
		yield return new WaitForSeconds(0.2f);
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.enabled = false;
		
		replaceState = false;
	}
}
