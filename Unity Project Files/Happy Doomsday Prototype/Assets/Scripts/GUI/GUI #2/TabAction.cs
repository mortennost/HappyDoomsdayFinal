using UnityEngine;
using System.Collections;
using iGUI;

public class TabAction : iGUIAction 
{
	public Texture turretTabImage;
	public Texture harvesterTabImage;
	public Texture utilityTabImage;
	public Texture realMoneyTabImage;
	
	[HideInInspector]
	public Texture currentImage;

	public override void act (iGUIElement caller)
	{		
		if(caller == GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnTurretsCategory)
		{
			GameObject.Find("1-OpenShop Button").GetComponent<EnableMenuState>().currentImage = turretTabImage;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgShopBackground.image = GameObject.Find("1-OpenShop Button").GetComponent<EnableMenuState>().currentImage;
		}
		else if(caller == GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvestersCategory)
		{
			GameObject.Find("1-OpenShop Button").GetComponent<EnableMenuState>().currentImage = harvesterTabImage;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgShopBackground.image = GameObject.Find("1-OpenShop Button").GetComponent<EnableMenuState>().currentImage;
		}
		else if(caller == GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUtilityCategory)
		{
			GameObject.Find("1-OpenShop Button").GetComponent<EnableMenuState>().currentImage = utilityTabImage;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgShopBackground.image = GameObject.Find("1-OpenShop Button").GetComponent<EnableMenuState>().currentImage;
		}
		else if(caller == GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnRealMoneyCategory)
		{
			GameObject.Find("1-OpenShop Button").GetComponent<EnableMenuState>().currentImage = harvesterTabImage;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgShopBackground.image = GameObject.Find("1-OpenShop Button").GetComponent<EnableMenuState>().currentImage;
		}
	}
}
