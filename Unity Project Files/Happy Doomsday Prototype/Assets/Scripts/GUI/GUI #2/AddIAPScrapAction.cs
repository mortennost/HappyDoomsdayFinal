using UnityEngine;
using System.Collections;
using iGUI;

public class AddIAPScrapAction : iGUIAction 
{
	public int additionalScrap;
	
	public override void act (iGUIElement caller)
	{
		GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddScrap(additionalScrap);
		
		GameObject.Find("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("" + additionalScrap + " Scrap added to your account. \nThank you for your purchase!");
	}
}
