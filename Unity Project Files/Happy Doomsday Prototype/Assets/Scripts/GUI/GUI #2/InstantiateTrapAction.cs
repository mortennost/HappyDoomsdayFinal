using UnityEngine;
using System.Collections;
using iGUI;

public class InstantiateTrapAction : iGUIAction
{
	public GameObject prefab;
	
	Vector3 highlightXScale;
	Vector3 highlightXPos;
	Vector3 highlightZScale;
	Vector3 highlightZPos;
	
	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
	}
	
	public override void act (iGUIElement caller)
	{
		PlaceTrap();
	}
	
	public void PlaceTrap ()
	{
		if(prefab.GetComponent<StructureScript>().baseFoodCost <= GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetFood() &&
			prefab.GetComponent<StructureScript>().baseWaterCost <= GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWater() &&
			prefab.GetComponent<StructureScript>().baseScrapMetalCost <= GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetScrap())
		{
			// reposition gridsnapper at camera position
			Ray currentRay = Camera.mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			float currentEnter = 0.0f;
			plane.Raycast(currentRay, out currentEnter);	
			Vector3 currentPosition = currentRay.origin + currentEnter * currentRay.direction;
			GameObject.Find("Gridsnapper").transform.position = currentPosition;
			
			
			Camera.mainCamera.GetComponent<CameraScript>().target = GameObject.Find("Gridsnapper");
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelTraps.enabled = false;
			
			// Instantiate object from prefab
			GameObject instance = (GameObject)Instantiate(prefab);
			
			// Attach structure-object to clone
			GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone = instance;
			
			GameObject.Find("Input Manager").GetComponent<InputHandler>().SwitchToBuildTrapInputLayer();
			
			// Set GridSnapper size to 1x1
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
			
			// Enable buttons to place/cancel structure
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceTrap.setOpacity(1.0f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelTrap.setOpacity(1.0f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceTrap.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelTrap.enabled = true;
		}
		else
		{
			GameObject.Find("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Can't build trap. \nNot enough resources!");
		}
	}
}
