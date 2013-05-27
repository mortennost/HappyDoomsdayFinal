using UnityEngine;
using System.Collections;
using iGUI;

public class UpgradeStructureAction : iGUIAction 
{
	[HideInInspector]
	public GameObject obj;
	
	void Update()
	{
		if(obj != null)
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotificationUpgradeFoodCost.label.text = "" + obj.GetComponent<StructureScript>().FoodCost;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotificationUpgradeWaterCost.label.text = "" + obj.GetComponent<StructureScript>().WaterCost;
		}
	}
	
	public override void act (iGUIElement caller)
	{
		if(obj.GetComponent<StructureScript>().FoodCost <= GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetFood() &&
			obj.GetComponent<StructureScript>().WaterCost <= GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWater())
		{
			if(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWorkerCount() < 2)
			{
				GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().DepleteFood(obj.GetComponent<StructureScript>().FoodCost);
				GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().DepleteWater(obj.GetComponent<StructureScript>().WaterCost);
				UpgradeStructure(obj);
				
				if(!obj.GetComponent<StructureScript>().structureType.Equals("Playerhouse"))
				{
					//GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().Scale(obj, GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().normalScale);
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.setOpacity(1.0f);
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.enabled = true;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled = false;
				}
				
				//Camera.mainCamera.GetComponent<CameraScript>().target = null;
			}
			else
			{
				GameObject.Find("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Not enough available workers!");
			}
		}
		else
		{
			GameObject.Find("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Not enough resources!");
		}
	}
	
	public void UpgradeStructure(GameObject obj)
	{
		if(obj.GetComponent<StructureStateManager>() != null)
		{
			if(obj.GetComponent<StructureStateManager>().GetPeek().ToString().Equals("AIStateStructureOperational") &&
				obj.GetComponent<Level>().GetLevel() < GameObject.Find("Playerhouse").GetComponent<Level>().GetLevel())
			{
				// Add XP
				GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddExperience(obj.GetComponent<StructureScript>()._experienceGain);
				
				obj.GetComponent<Level>().IncreaseLevel();
				
				obj.GetComponent<StructureStateManager>().ChangeState(new AIStateStructureCreation(obj));
			}
		}
		else if(obj.tag == "Playerhouse")
		{
			// Add XP
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddExperience(obj.GetComponent<StructureScript>()._experienceGain);
			
			obj.GetComponent<Level>().IncreaseLevel();
		}
	}
}
