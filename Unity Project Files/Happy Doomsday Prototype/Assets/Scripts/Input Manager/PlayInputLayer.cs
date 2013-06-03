using UnityEngine;
using System.Collections;

public class PlayInputLayer : InputLayer
{
	//InputHandler inputHandler;
	CameraScript cameraScript;
	
#if UNITY_IPHONE
	float touchPhaseTimer;
#endif
	
	Vector3 north;
	Vector3 south;
	Vector3 east;
	Vector3 west;
	
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
	
	Rect instaBuildConfirm;
	Rect instaBuildDecline;
	Rect upgradeConfirm;
	Rect upgradeDecline;
	Rect destroyConfirm;
	Rect destroyDecline;
	
	Rect shopRect;
	Rect notification;
	Rect optionsRect;
	Rect convertScrap;
	Rect credits;
	Rect trapsInfo;
	Rect traps;
	
	//[HideInInspector]
	public Vector3 selectedScale = new Vector3(1.6f, 1.6f, 1.6f);
	//[HideInInspector]
	public Vector3 normalScale = new Vector3(1.4f, 1.4f, 1.4f);

	// Use this for initialization
	void Start ()
	{
		//inputHandler = gameObject.GetComponent<InputHandler>();
		cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
		
#if UNITY_IPHONE
		touchPhaseTimer = 0.0f;
#endif
		
		north = Vector3.forward + Vector3.right;
		north.Normalize();
		south = -north;
		
		west = Vector3.forward + Vector3.left;
		west.Normalize();
		east = -west;
		
		shopRect = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelShop.rect;
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
		
		instaBuildConfirm = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationInstaBuildConfirm.rect;
		instaBuildDecline = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationInstaBuildDecline.rect;
		upgradeConfirm = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationUpgradeConfirm.rect;
		upgradeDecline = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationUpgradeDecline.rect;
		destroyConfirm = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationDestroyConfirm.rect;
		destroyDecline = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationDestroyDecline.rect;
	}
	
	// Update is called once per frame
	void Update()
	{
		
#if UNITY_IPHONE
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			touchPhaseTimer = 0.0f;
		}
		
		if (Input.touchCount > 0)
		{
			touchPhaseTimer += Input.GetTouch(0).deltaTime;
		}
		
		// Pinch Zoom
		if (Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved))
		{
			Vector2 currentDistance = Input.GetTouch(0).position - Input.GetTouch(1).position;
			Vector2 previousDistance = (Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
			float touchDelta = currentDistance.magnitude - previousDistance.magnitude;
			
			Camera.mainCamera.orthographicSize -= touchDelta * 0.01f;
			if (Camera.mainCamera.orthographicSize < 3.0f) Camera.mainCamera.orthographicSize = 3.0f;
			if (Camera.mainCamera.orthographicSize > 9.0f) Camera.mainCamera.orthographicSize = 9.0f;
		}
		
		// Swipe Pan
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && cameraScript.target == null)
		{
			//if (cameraScript.target == null)
			//{
			//	Debug.Log("Camera target is NULL, swiping is POSSIBLE!");
			//}
			
			Ray currentRay = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
			Ray previousRay = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
			
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			
			float currentEnter = 0.0f;
			float previousEnter = 0.0f;
			
			plane.Raycast(currentRay, out currentEnter);
			plane.Raycast(previousRay, out previousEnter);
			
			Vector3 currentPosition = currentRay.origin + currentEnter * currentRay.direction;
			Vector3 previousPosition = previousRay.origin + previousEnter * previousRay.direction;
			Vector3 deltaPosition = currentPosition - previousPosition;
			
			Vector3 newPosition = Camera.mainCamera.transform.position - deltaPosition;
			newPosition = CheckBounds(newPosition);
			
			cameraScript.SetDesiredPosition(newPosition);
			Camera.mainCamera.transform.position = newPosition;
		}
		
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
						if(cameraScript.target != null)
						{
							Scale( cameraScript.target, normalScale );
						}
						
						cameraScript.target = null;
						cameraScript.target = obj;
						found = true;
						obj.GetComponent<StructureScript>().OpenSubMenu(obj);
						
						// check if the object is texture based
						
						Scale ( obj, selectedScale );
						
						break;
					}
					else
					{
						if(obj.transform.FindChild("TargetRadius") != null)
						{
							obj.transform.FindChild("TargetRadius").gameObject.SetActive(false);
						}
						
						Scale ( obj, normalScale );
						
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
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.fadeTo(0.0f, 0.2f);
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblBuildTimer.fadeTo(0.0f, 0.2f);
					
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblBuildTimer.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgStructureHealthFill.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureHealth.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgHarvestProgressBG.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgHarvestProgressFill.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblHarvestProgress.enabled = false;
					
					// Deactivate TargetRadius-indicator
					if(cameraScript.target != null && cameraScript.target.transform.FindChild("TargetRadius") != null)
					{
						cameraScript.target.transform.FindChild("TargetRadius").gameObject.SetActive(false);
					}
										
					cameraScript.target = null;
				}
			}
			
			touchPhaseTimer = 0.0f;
		}
#endif
		
#if UNITY_WEBPLAYER
		
		if (Input.GetKeyDown(KeyCode.I))
		{
			GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new InvasionState(GameObject.Find("GameStateManager")));
		}
		
		if (Input.GetKeyDown(KeyCode.G))
		{
			if(GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelGodMode.enabled)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelGodMode.enabled = false;
			}
			else
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelGodMode.enabled = true;
			}
		}
		
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
					if(cameraScript.target != null)
					{
						Scale( cameraScript.target, normalScale );
					}
					
					cameraScript.target = null;
					cameraScript.target = obj;
					found = true;
					obj.GetComponent<StructureScript>().OpenSubMenu(obj);
					
					// check if the object is texture based
					
					Scale ( obj, selectedScale );
					
					break;
				}
				else
				{
					if(obj.transform.FindChild("TargetRadius") != null)
					{
						obj.transform.FindChild("TargetRadius").gameObject.SetActive(false);
					}
					
					Scale ( obj, normalScale );
					
				}
				
				//obj.transform.localScale = normalScale;
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
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.fadeTo(0.0f, 0.2f);
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblBuildTimer.fadeTo(0.0f, 0.2f);
				
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblBuildTimer.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgStructureHealthFill.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureHealth.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgHarvestProgressBG.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgHarvestProgressFill.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblHarvestProgress.enabled = false;
				
				// Deactivate TargetRadius-indicator
				if(cameraScript.target != null && cameraScript.target.transform.FindChild("TargetRadius") != null)
				{
					cameraScript.target.transform.FindChild("TargetRadius").gameObject.SetActive(false);
				}
				
				cameraScript.target = null;
			}
		}
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			Vector3 newPosition = cameraScript.GetDesiredPosition() + north * 20.0f * Time.deltaTime;
			newPosition = CheckBounds(newPosition);
			//Debug.Log(newPosition);
			cameraScript.SetDesiredPosition(newPosition);
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			Vector3 newPosition = cameraScript.GetDesiredPosition() + south * 20.0f * Time.deltaTime;
			newPosition = CheckBounds(newPosition);
			cameraScript.SetDesiredPosition(newPosition);
		}
		
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			Vector3 newPosition = cameraScript.GetDesiredPosition() + west * 20.0f * Time.deltaTime;
			newPosition = CheckBounds(newPosition);
			cameraScript.SetDesiredPosition(newPosition);
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			Vector3 newPosition = cameraScript.GetDesiredPosition() + east * 20.0f * Time.deltaTime;
			newPosition = CheckBounds(newPosition);
			cameraScript.SetDesiredPosition(newPosition);
		}
#endif
		//} //Remove this for Web-player, keep bracket for iOS
	}
	
	Vector3 CheckBounds(Vector3 newPosition)
	{
		Vector3 check = newPosition + Camera.mainCamera.transform.forward * cameraScript.distance;
		
		if (check.x < 0.0f) newPosition.x += Mathf.Abs(check.x);
		if (check.x > 32.0f) newPosition.x -= Mathf.Abs(check.x - 32.0f);
		if (check.z < 0.0f) newPosition.z += Mathf.Abs(check.z);
		if (check.z > 32.0f) newPosition.z -= Mathf.Abs(check.z - 32.0f);
		
		return newPosition;
	}
	
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
		
		instaBuildConfirm = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationInstaBuildConfirm.rect;
		instaBuildDecline = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationInstaBuildDecline.rect;
		upgradeConfirm = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationUpgradeConfirm.rect;
		upgradeDecline = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationUpgradeDecline.rect;
		destroyConfirm = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationDestroyConfirm.rect;
		destroyDecline = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationDestroyDecline.rect;
		
		shopRect = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelShop.rect;
		notification = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelNotificationInfo.rect;
		optionsRect = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelOptions.rect;
		convertScrap = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelConvertScrap.rect;
		credits = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelCredits.rect;
		trapsInfo = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelTrapsInfo.rect;
		traps = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelTraps.rect;
		
		
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
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCloseShop.enabled == true) &&
			
				!(screenPosition.x > instaBuildConfirm.x &&
				screenPosition.x < instaBuildConfirm.x + instaBuildConfirm.width &&
				screenPosition.y > instaBuildConfirm.y &&
				screenPosition.y < instaBuildConfirm.y + instaBuildConfirm.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationInstaBuildConfirm.enabled == true) &&
			
				!(screenPosition.x > instaBuildDecline.x &&
				screenPosition.x < instaBuildDecline.x + instaBuildDecline.width &&
				screenPosition.y > instaBuildDecline.y &&
				screenPosition.y < instaBuildDecline.y + instaBuildDecline.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationInstaBuildDecline.enabled == true) &&
				
				!(screenPosition.x > upgradeConfirm.x &&
				screenPosition.x < upgradeConfirm.x + upgradeConfirm.width &&
				screenPosition.y > upgradeConfirm.y &&
				screenPosition.y < upgradeConfirm.y + upgradeConfirm.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationUpgradeConfirm.enabled == true) &&
			
				!(screenPosition.x > upgradeDecline.x &&
				screenPosition.x < upgradeDecline.x + upgradeDecline.width &&
				screenPosition.y > upgradeDecline.y &&
				screenPosition.y < upgradeDecline.y + upgradeDecline.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationUpgradeDecline.enabled == true) &&
			
				!(screenPosition.x > destroyConfirm.x &&
				screenPosition.x < destroyConfirm.x + destroyConfirm.width &&
				screenPosition.y > destroyConfirm.y &&
				screenPosition.y < destroyConfirm.y + destroyConfirm.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationDestroyConfirm.enabled == true) &&
			
				!(screenPosition.x > destroyDecline.x &&
				screenPosition.x < destroyDecline.x + destroyDecline.width &&
				screenPosition.y > destroyDecline.y &&
				screenPosition.y < destroyDecline.y + destroyDecline.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnNotificationDestroyDecline.enabled == true) &&
			
				!(screenPosition.x > shopRect.x &&
				screenPosition.x < shopRect.x + shopRect.width &&
				screenPosition.y > shopRect.y &&
				screenPosition.y < shopRect.y + shopRect.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCloseShop.enabled == true) &&
				
				!(screenPosition.x > notification.x &&
				screenPosition.x < notification.x + notification.width &&
				screenPosition.y > notification.y &&
				screenPosition.y < notification.y + notification.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelNotificationInfo.enabled == true) &&
				
				!(screenPosition.x > optionsRect.x &&
				screenPosition.x < optionsRect.x + optionsRect.width &&
				screenPosition.y > optionsRect.y &&
				screenPosition.y < optionsRect.y + optionsRect.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelOptions.enabled == true) &&
				
				!(screenPosition.x > convertScrap.x &&
				screenPosition.x < convertScrap.x + convertScrap.width &&
				screenPosition.y > convertScrap.y &&
				screenPosition.y < convertScrap.y + convertScrap.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelConvertScrap.enabled == true) &&
				
				!(screenPosition.x > credits.x &&
				screenPosition.x < credits.x + credits.width &&
				screenPosition.y > credits.y &&
				screenPosition.y < credits.y + credits.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelCredits.enabled == true) &&
				
				!(screenPosition.x > trapsInfo.x &&
				screenPosition.x < trapsInfo.x + trapsInfo.width &&
				screenPosition.y > trapsInfo.y &&
				screenPosition.y < trapsInfo.y + trapsInfo.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelTrapsInfo.enabled == true) &&
				
				!(screenPosition.x > traps.x &&
				screenPosition.x < traps.x + traps.width &&
				screenPosition.y > traps.y &&
				screenPosition.y < traps.y + traps.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelTraps.enabled == true)
		)
		{
		
			return false;
		}
		else
		{
			return true;
		}
		
	}
	
	public void Scale( GameObject o, Vector3 scale )
	{
		
		if ( o.GetComponent<UpdateTextureScript>() )
		{ // the object is Texture based
			o.transform.FindChild( "Texture" ).transform.localScale = scale;
			
		}
		else if ( o.GetComponent<TextureOffsetScript>() )
		{
			// do nothing here remove this when we no longer use TextureOffsetScript
		}
		else
		{ // the object is model based
			
			o.transform.FindChild( "Model" ).transform.localScale = scale;
			o.transform.FindChild( "Model2" ).transform.localScale = scale;
			o.transform.FindChild( "Model3" ).transform.localScale = scale;
			
		}
		
	}
}
