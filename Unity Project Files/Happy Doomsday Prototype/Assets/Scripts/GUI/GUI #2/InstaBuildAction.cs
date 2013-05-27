using UnityEngine;
using System.Collections;
using iGUI;

public class InstaBuildAction : iGUIAction 
{
	[HideInInspector]
	public GameObject obj;
	
	void Update()
	{
		if(obj != null)
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotificationInstaBuildResourceCost.label.text = "" + obj.GetComponent<StructureScript>().InstaBuildCost;
		}
	}
	
	public override void act (iGUIElement caller)
	{		
		if(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetScrap() >= obj.GetComponent<StructureScript>().InstaBuildCost)
		{
			InstaBuild(obj);
			//Camera.mainCamera.GetComponent<CameraScript>().target = null;
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().DepleteScrap(obj.GetComponent<StructureScript>().InstaBuildCost);
			
			//GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().Scale(obj, GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().normalScale);
			
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.enabled = false;
			
			if(obj.GetComponent<Level>().GetLevel() < GameObject.Find("Playerhouse").GetComponent<Level>().GetLevel())
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.setOpacity(1.0f);
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled = true;
				GameObject.Find("3-NotificationUpgradeConfirm Button").GetComponent<UpgradeStructureAction>().obj = obj;
			}
		}
		else
		{
			GameObject.Find("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Not enough resources!");
		}
		
		//obj.transform.localScale = GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().normalScale;
	}
	
	public void InstaBuild(GameObject obj)
	{
		if(obj.GetComponent<StructureStateManager>().GetPeek().ToString().Equals("AIStateStructureCreation"))
		{
			AIStateStructureCreation temp = obj.GetComponent<StructureStateManager>().GetPeek() as AIStateStructureCreation;
			temp.InstaBuild();
		}
	}
}
