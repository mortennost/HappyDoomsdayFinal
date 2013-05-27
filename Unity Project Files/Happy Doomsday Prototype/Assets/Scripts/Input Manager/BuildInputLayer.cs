using UnityEngine;
using System.Collections;

public class BuildInputLayer : InputLayer {
	
	//InputHandler inputHandler;
	
	Vector3 swipeBeganPosition;
	Vector3 snapperSwipeBeganPosition;
	
	float touchPhaseTimer;
	
	Rect shop;
	Rect shopClose;
	Rect place;
	Rect cancel;
	Rect options;

	// Use this for initialization
	void Start()
	{
		//inputHandler = gameObject.GetComponent<InputHandler>();
		swipeBeganPosition = Vector3.zero;
		snapperSwipeBeganPosition = GameObject.Find("Gridsnapper").transform.position;
		
		touchPhaseTimer = 0.0f;
		
		shop = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOpenShop.rect;
		shopClose = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCloseShop.rect;
		place = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.rect;
		cancel = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.rect;
		options = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOptions.rect;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (CanPlaceBuilding())
		{
			GameObject.Find("ConstructionHighlight X").GetComponent<HighlightPositionScript>().renderer.material.color = new Color(0.0f, 1.0f, 0.0f, 0.25f);
			GameObject.Find("ConstructionHighlight Z").GetComponent<HighlightPositionScript>().renderer.material.color = new Color(0.0f, 1.0f, 0.0f, 0.25f);
		}
		else
		{
			GameObject.Find("ConstructionHighlight X").GetComponent<HighlightPositionScript>().renderer.material.color = new Color(1.0f, 0.0f, 0.0f, 0.25f);
			GameObject.Find("ConstructionHighlight Z").GetComponent<HighlightPositionScript>().renderer.material.color = new Color(1.0f, 0.0f, 0.0f, 0.25f);
		}
		
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			touchPhaseTimer = 0.0f;
			
			Ray currentRay = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			float currentEnter = 0.0f;
			plane.Raycast(currentRay, out currentEnter);
			swipeBeganPosition = currentRay.origin + currentEnter * currentRay.direction;
			snapperSwipeBeganPosition = GameObject.Find("Gridsnapper").transform.position;
		}
		
		if (Input.touchCount > 0)
		{
			touchPhaseTimer += Input.GetTouch(0).deltaTime;
		}
		
		// Swipe
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Ray currentRay = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			float currentEnter = 0.0f;
			
			plane.Raycast(currentRay, out currentEnter);
			
			Vector3 currentPosition = currentRay.origin + currentEnter * currentRay.direction;
			Vector3 deltaPosition = currentPosition - swipeBeganPosition;
			
			deltaPosition *= 0.6f;
			
			GameObject.Find("Gridsnapper").transform.position = snapperSwipeBeganPosition + deltaPosition;
			GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().Snap();
		}
		
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
				
				GameObject.Find("Gridsnapper").transform.position = currentPosition;
				GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().Snap();
			}
		}
		
		
#if UNITY_WEBPLAYER
		
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{						
			if (!IsInsideGUIElement(Input.mousePosition))
			{
				Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
				
				float enter = 0.0f;
				Plane plane = new Plane(Vector3.up, Vector3.zero);
				if (plane.Raycast(ray, out enter))
				{
					GameObject.Find("Gridsnapper").transform.position = ray.origin + ray.direction * enter;
				}
			}
		}
		
#endif
		
		
		
		if (Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved))
		{
			Vector2 currentDistance = Input.GetTouch(0).position - Input.GetTouch(1).position;
			Vector2 previousDistance = (Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
			float touchDelta = currentDistance.magnitude - previousDistance.magnitude;
			
			Camera.mainCamera.orthographicSize -= touchDelta * 0.01f;
			if (Camera.mainCamera.orthographicSize < 1.0f) Camera.mainCamera.orthographicSize = 1.0f;
			if (Camera.mainCamera.orthographicSize > 5.0f) Camera.mainCamera.orthographicSize = 5.0f;
		}
	}
	
	bool IsInsideGUIElement(Vector2 screenPosition)
	{
		shop = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOpenShop.rect;
		shopClose = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCloseShop.rect;
		place = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.rect;
		cancel = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.rect;
		options = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOptions.rect;
		
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
			
				!(screenPosition.x > options.x &&
				screenPosition.x < options.x + options.width &&
				screenPosition.y > options.y &&
				screenPosition.y < options.y + options.height &&
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOptions.enabled == true) &&
			
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
	
	bool CanPlaceBuilding()
	{
		GameObject structureToBeBuilt = GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone;
		StructureScript toBeBuiltScript = structureToBeBuilt.GetComponent<StructureScript>();
		Vector3 toBeBuiltPosition = GameObject.Find("Gridsnapper").transform.position;
		//int xSize = toBeBuiltScript.xSize;
		//int zSize = toBeBuiltScript.zSize;
		
		float xEMin = float.PositiveInfinity;
		float xEMax = float.NegativeInfinity;
		float zEMin = float.PositiveInfinity;
		float zEMax = float.NegativeInfinity;
		
		float xTMin = float.PositiveInfinity;
		float xTMax = float.NegativeInfinity;
		float zTMin = float.PositiveInfinity;
		float zTMax = float.NegativeInfinity;
		
		//bool isOnExistingBuilding = false;
		
		GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
		
		foreach(GameObject existingStructure in structures)
		{			
			StructureScript existingScript = existingStructure.GetComponent<StructureScript>();
			Vector3 existingPosition = existingStructure.transform.position;
			
			xEMin = existingPosition.x - (float) existingScript.xSize / 2.0f;
			xEMax = existingPosition.x + (float) existingScript.xSize / 2.0f;
			zEMin = existingPosition.z - (float) existingScript.zSize / 2.0f;
			zEMax = existingPosition.z + (float) existingScript.zSize / 2.0f;
			
			xTMin = toBeBuiltPosition.x - (float) toBeBuiltScript.xSize / 2.0f;
			xTMax = toBeBuiltPosition.x + (float) toBeBuiltScript.xSize / 2.0f;
			zTMin = toBeBuiltPosition.z - (float) toBeBuiltScript.zSize / 2.0f;
			zTMax = toBeBuiltPosition.z + (float) toBeBuiltScript.zSize / 2.0f;
			
			if (
				((xTMin >= xEMin && xTMin < xEMax) || (xTMax <= xEMax && xTMax > xEMin)) &&
				((zTMin >= zEMin && zTMin < zEMax) || (zTMax <= zEMax && zTMax > zEMin)) &&
				existingStructure != structureToBeBuilt
				)
			{
				return false;
			}
		}
		
		structures = GameObject.FindGameObjectsWithTag("Playerhouse");
		
		foreach(GameObject existingStructure in structures)
		{			
			StructureScript existingScript = existingStructure.GetComponent<StructureScript>();
			Vector3 existingPosition = existingStructure.transform.position;
			
			xEMin = existingPosition.x - (float) existingScript.xSize / 2.0f;
			xEMax = existingPosition.x + (float) existingScript.xSize / 2.0f;
			zEMin = existingPosition.z - (float) existingScript.zSize / 2.0f;
			zEMax = existingPosition.z + (float) existingScript.zSize / 2.0f;
			
			xTMin = toBeBuiltPosition.x - (float) toBeBuiltScript.xSize / 2.0f;
			xTMax = toBeBuiltPosition.x + (float) toBeBuiltScript.xSize / 2.0f;
			zTMin = toBeBuiltPosition.z - (float) toBeBuiltScript.zSize / 2.0f;
			zTMax = toBeBuiltPosition.z + (float) toBeBuiltScript.zSize / 2.0f;
			
			if (
				((xTMin >= xEMin && xTMin < xEMax) || (xTMax <= xEMax && xTMax > xEMin)) &&
				((zTMin >= zEMin && zTMin < zEMax) || (zTMax <= zEMax && zTMax > zEMin))
				)
			{
				return false;
			}
		}
		
		return true;
	}
}
