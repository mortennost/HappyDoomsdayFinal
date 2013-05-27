using UnityEngine;
using System.Collections;

public class MenuInputLayer : InputLayer
{
	
	//InputHandler inputHandler;
	
	/*
	CameraScript cameraScript;
	float touchPhaseTimer;
	
	Rect shop;
	Rect shopClose;
	Rect place;
	Rect cancel;
	Rect options;
	Rect harvest;
	Rect destroy;
	Rect replace;
	Rect upgrade;
	Rect instant;
	*/

	// Use this for initialization
	void Start ()
	{
		/*
		//inputHandler = gameObject.GetComponent<InputHandler>();
		cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
		touchPhaseTimer = 0.0f;
		
		shop = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOpenShop.rect;
		shopClose = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCloseShop.rect;
		place = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.rect;
		cancel = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.rect;
		options = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOptions.rect;
		harvest = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.rect;
		destroy = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.rect;
		replace = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.rect;
		upgrade = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.rect;
		instant = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.rect;
		*/
	}
	
	// Update is called once per frame
	void Update()
	{
		/*
		// Tap on structure (iOS)
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{			
			// Tap
			if (touchPhaseTimer < 0.5f && !IsInsideGUIElement(Input.GetTouch(0).position))
			{
				Ray currentRay = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
				Plane plane = new Plane(Vector3.up, Vector3.zero);
				float currentEnter = 0.0f;
				plane.Raycast(currentRay, out currentEnter);
				
				Vector3 currentPosition = currentRay.origin + currentEnter * currentRay.direction;
				
				GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
				GameObject playerHouse = GameObject.FindGameObjectWithTag("Playerhouse");
				
				bool found = false;
				
				foreach (GameObject obj in structures)
				{
					// Check if raycast is within structure's bounds
					if(obj.GetComponent<StructureScript>().IsInsideBounds(currentPosition))
					{
						cameraScript.target = obj;
						found = true;
						obj.GetComponent<StructureScript>().OpenSubMenu(obj);
						break;
					}
				}
				
				if(playerHouse.GetComponent<StructureScript>().IsInsideBounds(currentPosition))
				{
					cameraScript.target = playerHouse;
					found = true;
					playerHouse.GetComponent<StructureScript>().OpenSubMenu(playerHouse);
				}
				
				if (!found) 
				{
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.fadeTo(0.0f, 0.2f);
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.fadeTo(0.0f, 0.2f);
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.fadeTo(0.0f, 0.2f);
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.fadeTo(0.0f, 0.2f);
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.fadeTo(0.0f, 0.2f);
					
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.enabled = false;
					
					cameraScript.target = null;
				}
			}
			
			touchPhaseTimer = 0.0f;
		}
		
		
#if UNITY_WEBPLAYER	
		// Click on structure, open SubMenu (PC)
		if (Input.GetKeyDown(KeyCode.Mouse0) && !IsInsideGUIElement(Input.mousePosition)) // Click on structure (PC)
		{			
			Ray currentRay = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			float currentEnter = 0.0f;
			plane.Raycast(currentRay, out currentEnter);
			
			Vector3 currentPosition = currentRay.origin + currentEnter * currentRay.direction;
			
			GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
			GameObject playerHouse = GameObject.FindGameObjectWithTag("Playerhouse");
			
			bool found = false;
				
			foreach (GameObject obj in structures)
			{
				// Check if raycast is within structure's bounds
				if(obj.GetComponent<StructureScript>().IsInsideBounds(currentPosition))
				{
					cameraScript.target = obj;
					found = true;
					obj.GetComponent<StructureScript>().OpenSubMenu(obj);
					break;
				}
			}
			
			if(playerHouse.GetComponent<StructureScript>().IsInsideBounds(currentPosition))
			{
				cameraScript.target = playerHouse;
				found = true;
				playerHouse.GetComponent<StructureScript>().OpenSubMenu(playerHouse);
			}
				
			if (!found) 
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.fadeTo(0.0f, 0.2f);
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.fadeTo(0.0f, 0.2f);
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.fadeTo(0.0f, 0.2f);
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.fadeTo(0.0f, 0.2f);
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.fadeTo(0.0f, 0.2f);
				
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.enabled = false;
				
				cameraScript.target = null;
			}
		}
#endif
*/
	}
	
	/*
	bool IsInsideGUIElement(Vector2 screenPosition)
	{
		shop = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOpenShop.rect;
		shopClose = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCloseShop.rect;
		place = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.rect;
		cancel = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.rect;
		options = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOptions.rect;
		harvest = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.rect;
		destroy = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.rect;
		replace = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.rect;
		upgrade = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.rect;
		instant = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.rect;
		
		
		screenPosition.y = Screen.height - screenPosition.y;
		
		if (
				!(screenPosition.x > cancel.x &&
				screenPosition.x < cancel.x + cancel.width &&
				screenPosition.y > cancel.y &&
				screenPosition.y < cancel.y + cancel.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.enabled == true) &&
				
				!(screenPosition.x > place.x &&
				screenPosition.x < place.x + place.width &&
				screenPosition.y > place.y &&
				screenPosition.y < place.y + place.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.enabled == true) &&
				
				!(screenPosition.x > shop.x &&
				screenPosition.x < shop.x + shop.width &&
				screenPosition.y > shop.y &&
				screenPosition.y < shop.y + shop.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOpenShop.enabled == true) &&
			
				!(screenPosition.x > harvest.x &&
				screenPosition.x < harvest.x + harvest.width &&
				screenPosition.y > harvest.y &&
				screenPosition.y < harvest.y + harvest.height && 
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.enabled == true) &&
			
				!(screenPosition.x > replace.x &&
				screenPosition.x < replace.x + replace.width &&
				screenPosition.y > replace.y &&
				screenPosition.y < replace.y + replace.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.enabled == true) &&
			
				!(screenPosition.x > options.x &&
				screenPosition.x < options.x + options.width &&
				screenPosition.y > options.y &&
				screenPosition.y < options.y + options.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOptions.enabled == true) &&
				
				!(screenPosition.x > destroy.x &&
				screenPosition.x < destroy.x + destroy.width &&
				screenPosition.y > destroy.y &&
				screenPosition.y < destroy.y + destroy.height && 
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.enabled == true) &&
			
				!(screenPosition.x > upgrade.x &&
				screenPosition.x < upgrade.x + upgrade.width &&
				screenPosition.y > upgrade.y &&
				screenPosition.y < upgrade.y + upgrade.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled == true) &&
			
				!(screenPosition.x > instant.x &&
				screenPosition.x < instant.x + instant.width &&
				screenPosition.y > instant.y &&
				screenPosition.y < instant.y + instant.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.enabled == true) &&
			
				!(screenPosition.x > shopClose.x &&
				screenPosition.x < shopClose.x + shopClose.width &&
				screenPosition.y > shopClose.y &&
				screenPosition.y < shopClose.y + shopClose.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCloseShop.enabled == true)
		)
		{
		
			return false;
		}
		else
		{
			return true;
		}
		
	}
	*/
}
