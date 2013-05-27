using UnityEngine;
using System.Collections;
using iGUI;

public class PlaceTrapAction : iGUIAction 
{	
	public override void act(iGUIElement caller)
	{		
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelTraps.enabled = true;
		
		Vector3 toBeBuiltPosition = GameObject.Find("Gridsnapper").transform.position;
		int xSize = 1;
		int zSize = 1;
		
		float xEMin = float.PositiveInfinity;
		float xEMax = float.NegativeInfinity;
		float zEMin = float.PositiveInfinity;
		float zEMax = float.NegativeInfinity;
		
		float xTMin = float.PositiveInfinity;
		float xTMax = float.NegativeInfinity;
		float zTMin = float.PositiveInfinity;
		float zTMax = float.NegativeInfinity;
		
		bool isOnExistingBuilding = false;
		
		GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
		GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");
		
		foreach(GameObject existingStructure in structures)
		{			
			//StructureScript existingScript = existingStructure.GetComponent<StructureScript>();
			Vector3 existingPosition = existingStructure.transform.position;
			
			//Debug.Log("CHECK THIS OUT: " + existingStructure.GetComponent<StructureScript>().xSize);
			
			xEMin = existingPosition.x - (float) existingStructure.GetComponent<StructureScript>().xSize / 2.0f;
			xEMax = existingPosition.x + (float) existingStructure.GetComponent<StructureScript>().xSize / 2.0f;
			zEMin = existingPosition.z - (float) existingStructure.GetComponent<StructureScript>().xSize / 2.0f;
			zEMax = existingPosition.z + (float) existingStructure.GetComponent<StructureScript>().xSize / 2.0f;
			
			xTMin = toBeBuiltPosition.x - (float) xSize / 2.0f;
			xTMax = toBeBuiltPosition.x + (float) xSize / 2.0f;
			zTMin = toBeBuiltPosition.z - (float) zSize / 2.0f;
			zTMax = toBeBuiltPosition.z + (float) zSize / 2.0f;
			
			if (
				((xTMin >= xEMin && xTMin < xEMax) || (xTMax <= xEMax && xTMax > xEMin)) &&
				((zTMin >= zEMin && zTMin < zEMax) || (zTMax <= zEMax && zTMax > zEMin))
				)
			{
				isOnExistingBuilding = true;
				
				GameObject.Find ("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Can't build here.");
				return;
			}
		}
		
		foreach(GameObject existingTrap in traps)
		{			
			//StructureScript existingScript = existingTrap.GetComponent<StructureScript>();
			Vector3 existingPosition = existingTrap.transform.position;
			
			xEMin = existingPosition.x - (float) xSize / 2.0f;
			xEMax = existingPosition.x + (float) xSize / 2.0f;
			zEMin = existingPosition.z - (float) zSize / 2.0f;
			zEMax = existingPosition.z + (float) zSize / 2.0f;
			
			xTMin = toBeBuiltPosition.x - (float) xSize / 2.0f;
			xTMax = toBeBuiltPosition.x + (float) xSize / 2.0f;
			zTMin = toBeBuiltPosition.z - (float) zSize / 2.0f;
			zTMax = toBeBuiltPosition.z + (float) zSize / 2.0f;
			
			if (
				((xTMin >= xEMin && xTMin < xEMax) || (xTMax <= xEMax && xTMax > xEMin)) &&
				((zTMin >= zEMin && zTMin < zEMax) || (zTMax <= zEMax && zTMax > zEMin))
				)
			{
				isOnExistingBuilding = true;
				
				GameObject.Find ("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Can't build here.");
				return;
			}
		}
		
		
		
		
		
		
		
		// Playerhouse collision check
		StructureScript house = GameObject.FindGameObjectWithTag("Playerhouse").GetComponent<StructureScript>();
		Vector3 housePosition = house.transform.position;
			
		xEMin = housePosition.x - (float) house.xSize / 2.0f;
		xEMax = housePosition.x + (float) house.xSize / 2.0f;
		zEMin = housePosition.z - (float) house.zSize / 2.0f;
		zEMax = housePosition.z + (float) house.zSize / 2.0f;
			
		xTMin = toBeBuiltPosition.x - (float) xSize / 2.0f;
		xTMax = toBeBuiltPosition.x + (float) xSize / 2.0f;
		zTMin = toBeBuiltPosition.z - (float) zSize / 2.0f;
		zTMax = toBeBuiltPosition.z + (float) zSize / 2.0f;
			
		if (
			((xTMin >= xEMin && xTMin < xEMax) || (xTMax <= xEMax && xTMax > xEMin)) &&
			((zTMin >= zEMin && zTMin < zEMax) || (zTMax <= zEMax && zTMax > zEMin))
			)
		{
			isOnExistingBuilding = true;
				
			GameObject.Find ("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Can't build here.");
			return;
		}
		
		
		
		
		
		
		if(!isOnExistingBuilding)
		{
			// Find the clone-object
			GameObject instance = GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone;
			
			instance.tag = "Trap";
			instance.GetComponent<TrapScript>().isPlaced = true;
			instance.GetComponent<Level>().SetLevel(GameObject.Find("Workshed(Clone)").GetComponent<Level>().GetLevel());
			
			// Deplete resources - Cost set in ResourceManager
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().DepleteFood(instance.GetComponent<StructureScript>().FoodCost);
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().DepleteWater(instance.GetComponent<StructureScript>().WaterCost);
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().DepleteScrap(instance.GetComponent<StructureScript>().ScrapMetalCost);
			
			// Add XP
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddExperience(instance.GetComponent<StructureScript>()._experienceGain);
			
			// Detach structure-object from clone and change to PlayState
			GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone = null;
			GameObject.Find("5-Traps Panel").GetComponent<TrapInputAction>().instantiated = false;
			
			GameObject.Find("ConstructionHighlight Z").renderer.enabled = false;
			GameObject.Find("ConstructionHighlight X").renderer.enabled = false;
			
			// Switch to PlayInputLayer
			GameObject.Find("Input Manager").GetComponent<InputHandler>().SwitchToPlayInputLayer();
						
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceTrap.fadeTo(0.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelTrap.fadeTo(0.0f, 0.2f);
			
			StartCoroutine(WaitAndDisable());
		}
	}
	
	IEnumerator WaitAndDisable()
	{
		yield return new WaitForSeconds(0.2f);
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceTrap.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelTrap.enabled = false;
	}
}
