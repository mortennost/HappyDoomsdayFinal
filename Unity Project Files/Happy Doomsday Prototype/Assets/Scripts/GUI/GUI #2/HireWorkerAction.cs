using UnityEngine;
using System.Collections;
using iGUI;

public class HireWorkerAction : iGUIAction 
{
	public int _hireCost;
	
	public override void act (iGUIElement caller)
	{
		if(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetScrap() >= _hireCost)
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddHiredWorker();
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().DepleteScrap(_hireCost);
		}
		else
		{
			GameObject.Find("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Not enough resources to hire new worker!");
		}
	}
}
