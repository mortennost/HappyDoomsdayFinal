using UnityEngine;
using System.Collections;
using iGUI;

public class EnableMenuState : iGUIAction 
{
	[HideInInspector]
	public Texture currentImage;

	public override void act(iGUIElement caller)
	{
		if(Camera.mainCamera.GetComponent<CameraScript>().target != null)
		{
			//Camera.mainCamera.GetComponent<CameraScript>().target.transform.localScale = GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().normalScale;
			GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().Scale(Camera.mainCamera.GetComponent<CameraScript>().target, GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().normalScale);
		}
		
		Camera.mainCamera.GetComponent<CameraScript>().target = null;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.enabled = false;
		
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.enabled = false;
			
		GameObject.Find("4-CancelBuilding Button").GetComponent<iGUIButton>().onClick[0].act(caller);
		GameObject.Find("4-CancelBuilding Button").GetComponent<iGUIButton>().onClick[1].act(caller);
		
		if(currentImage == null)
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgShopBackground.image = GameObject.Find("1-TurretsCategory Button").GetComponent<TabAction>().harvesterTabImage;
			//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._scrlHarvesters.enabled = true;
		}
		else
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgShopBackground.image = currentImage;
		}
			
		GameObject.Find("GameStateManager").GetComponent<GameStateManager>().PushState(new MenuState(GameObject.Find("GameStateManager")));
	}
}
