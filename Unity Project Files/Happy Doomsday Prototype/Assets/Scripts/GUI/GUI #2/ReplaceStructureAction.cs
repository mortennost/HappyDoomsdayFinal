using UnityEngine;
using System.Collections;
using iGUI;

public class ReplaceStructureAction : iGUIAction 
{
	[HideInInspector]
	public GameObject obj;
	
	public override void act (iGUIElement caller)
	{
		if(!GameObject.Find("GameStateManager").GetComponent<GameStateManager>().GetPeek().ToString().Equals("InvasionState"))
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnPlaceBuilding.setOpacity(1.0f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnCancelBuilding.enabled = false;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOpenShop.passive = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnOptions.passive = true;
			
			GameObject.Find("3-PlaceBuilding Button").GetComponent<PlaceBuildingAction>().replaceState = true;
			obj.tag = "Untagged";
			GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().clone = obj;
			
			// Set the graphs vertices traversable attribute to false
			StructureScript structureScript = obj.GetComponent<StructureScript>();
			Vector3 cornerPosition = obj.transform.position;
			cornerPosition.x -= structureScript.xSize / 2.0f;
			cornerPosition.z -= structureScript.zSize / 2.0f;
			GameObject.Find("Grid").GetComponent<GridScript>().DirGraph.ToggleTraversable(cornerPosition, structureScript.xSize, structureScript.zSize, true);
			
			GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new BuildState(GameObject.Find("GameStateManager")));
		}
		else
		{
			Debug.Log("Can't replace building during invasion!");
		}
		
		//obj.transform.localScale = GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().normalScale;
		GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().Scale(obj, GameObject.Find("Input Manager").GetComponent<PlayInputLayer>().normalScale);
	}
}
