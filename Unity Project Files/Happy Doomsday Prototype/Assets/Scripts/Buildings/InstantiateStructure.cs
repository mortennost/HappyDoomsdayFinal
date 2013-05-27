using UnityEngine;
using System.Collections;
using iGUI;

public class InstantiateStructure : iGUIAction
{
	public GameObject prefab;
	
	public int currentFood;
	public int currentWater;
	public int foodCost;
	public int waterCost;
	
	Vector3 highlightXScale;
	Vector3 highlightXPos;
	Vector3 highlightZScale;
	Vector3 highlightZPos;
	
	public override void init (iGUIElement caller)
	{
		currentFood = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetFood();
		currentWater = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWater();
		foodCost = prefab.GetComponent<StructureScript>().baseFoodCost;
		waterCost = prefab.GetComponent<StructureScript>().baseWaterCost;
		
		if((GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetFood() >= prefab.GetComponent<StructureScript>().baseFoodCost
			&& GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWater() >= prefab.GetComponent<StructureScript>().baseWaterCost))
		{
			caller.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			
			if(prefab.GetComponent<StructureScript>().levelRequirement > GameObject.Find("Playerhouse").GetComponent<Level>().GetLevel())
			{
				caller.backgroundColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
			}
		}
		else
		{
			caller.backgroundColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
		}
		
		base.init (caller);
	}
	
	public override void act(iGUIElement caller)
	{
		//Debug.Log(prefab.GetComponent<StructureScript>().FoodCost);
		
		// Check if we have enough resources to build structure
		if((GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetFood() >= prefab.GetComponent<StructureScript>().baseFoodCost)
			&& (GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWater() >= prefab.GetComponent<StructureScript>().baseWaterCost ))
		{
			
			// If we try to build a Workshed while one already exists in the compound, we return an error message
			if(prefab.ToString().CompareTo("Workshed") > 0)
			{
				if(GameObject.Find("Workshed(Clone)") != null)
				{
					GameObject.Find ("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Already have the maximum amount \n of Worksheds on the playfield.");
					return;
				}
			}
			
			if(prefab.GetComponent<StructureScript>().levelRequirement > GameObject.Find("Playerhouse").GetComponent<Level>().GetLevel())
			{
				GameObject.Find ("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Level too low: Upgrade your mainhouse to level " + prefab.GetComponent<StructureScript>().levelRequirement + " to access this structure.");
				return;
			}
			
			
			// Instantiate object from prefab
			GameObject instance = (GameObject)Instantiate(prefab);	
			
			// Set GridSnapper size
			GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().xSize = instance.GetComponent<StructureScript>().xSize;
			GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().zSize = instance.GetComponent<StructureScript>().zSize;
			
			// Set X-highlighter position and size
			highlightXScale = new Vector3(100.0f, 1.0f, 0.1f * (float)GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().zSize);
			highlightXPos = new Vector3(GameObject.Find("Gridsnapper").transform.position.x, 0.1f, GameObject.Find("Gridsnapper").transform.position.z);
			GameObject.Find("ConstructionHighlight X").transform.position = highlightXPos;
			GameObject.Find("ConstructionHighlight X").transform.localScale = highlightXScale;
			
			// Set Z-highlighter position and size
			highlightZScale = new Vector3(0.1f * (float)GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().xSize, 1.0f, 100.0f);
			highlightZPos = new Vector3(GameObject.Find("Gridsnapper").transform.position.x, 0.1f, GameObject.Find("Gridsnapper").transform.position.z);
			GameObject.Find("ConstructionHighlight Z").transform.position = highlightZPos;
			GameObject.Find("ConstructionHighlight Z").transform.localScale = highlightZScale;
			
			// Attach structure-object to clone
			GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone = instance;
			
			// Set material to the transparent (Transparent/Diffuse) material
			//instance.GetComponentInChildren<Renderer>().materials = instance.GetComponent<MaterialScript>().transparentMaterial;
			//if(instance.transform.FindChild("ParticleEffect") != null)
			//{
			//	instance.transform.FindChild("ParticleEffect").GetComponentInChildren<Renderer>().materials = instance.transform.FindChild("ParticleEffect").GetComponent<MaterialScript>().transparentMaterial;
			//}
				
			// Enable buttons to place/cancel structure
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.enabled = true;
			
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.fadeTo(1.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.fadeTo(1.0f, 0.2f);
	
			// Close the ShopMenu
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCloseShop.onClick[0].act(caller);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCloseShop.onClick[1].act(caller);
			
			// Change to BuildState
			GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new BuildState(GameObject.Find("GameStateManager")));
		}
		else
		{
			GameObject.Find ("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("You don't have enough resources \nto build this structure!");
		}
	}
}
