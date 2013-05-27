using UnityEngine;
using System.Collections;
using iGUI;

public class StructureScript : MonoBehaviour 
{
	
	public int xSize;
	public int zSize;
	
	public int levelRequirement;
	
	public string structureType = null;
	
	public float baseBuildTime;
	public float buildTimeModifier = 1.5f;
	
	public int baseFoodCost = 10;
	public int baseWaterCost = 10;
	public int baseScrapMetalCost = 0;
	public float foodCostModifier = 1.1f;
	public float waterCostModifier = 1.1f;
	public float scrapMetalCostModifier = 1.1f;
	
	public bool isBuilding;
	
	private float _buildTime;
	private int _waterCost;
	private int _foodCost;
	private int _scrapMetalCost;
	private int _instaBuildCost = 10;
	
	public int baseExperienceGain;
	
	[HideInInspector]
	public int _experienceGain;
	
	public int FoodCost {
		set { _foodCost = value; }
		get { return _foodCost; }
	}
	public int WaterCost {
		set { _waterCost = value; }
		get { return _waterCost; }
	}
	public int ScrapMetalCost {
		set { _scrapMetalCost = value; }
		get { return _scrapMetalCost; }
	}
	public float BuildTime {
		set { _buildTime = value; }
		get { return _buildTime; }
	}
	public int InstaBuildCost {
		set { _instaBuildCost = value; }
		get { return _instaBuildCost; }
	}
	
	
	bool upgradable;

	// Use this for initialization
	void Start () 
	{
		UpdateStats();
		upgradable = false;
		isBuilding = false;
	}
	
	public void UpdateStats()
	{
		int lvl = gameObject.GetComponent<Level>().GetLevel() - 1;
		
		FoodCost = ( int )( baseFoodCost * Mathf.Pow ( foodCostModifier, lvl ) );
		WaterCost = ( int )( baseWaterCost * Mathf.Pow ( waterCostModifier, lvl ) );
		ScrapMetalCost = ( int )( baseScrapMetalCost * Mathf.Pow ( scrapMetalCostModifier, lvl ) );
		BuildTime = ( int )( baseBuildTime * Mathf.Pow ( buildTimeModifier, lvl )  * 60 );
		_experienceGain = baseExperienceGain * gameObject.GetComponent<Level>().GetLevel();
		InstaBuildCost = (int)(baseBuildTime * 60);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public bool IsInsideBounds(Vector3 tapPosition)
	{
		float xMin = transform.position.x - xSize / 2.0f;
		float xMax = transform.position.x + xSize / 2.0f;
		float zMin = transform.position.z - zSize / 2.0f;
		float zMax = transform.position.z + zSize / 2.0f;
		
		if(tapPosition.x > xMin && tapPosition.x < xMax
			&& tapPosition.z > zMin && tapPosition.z < zMax)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public void OpenSubMenu(GameObject obj)
	{
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblBuildTimer.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgStructureHealthFill.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureHealth.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgHarvestProgressBG.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgHarvestProgressFill.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblHarvestProgress.enabled = false;
	
		if(obj.GetComponent<Level>().GetLevel() < GameObject.Find("Playerhouse").GetComponent<Level>().GetLevel())
		{
			upgradable = true;
		}
		else
		{
			upgradable = false;
		}
		
		if( structureType == "Harvester")
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.fadeTo(1.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.fadeTo(1.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgStructureHealthFill.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureHealth.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgHarvestProgressBG.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgHarvestProgressFill.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblHarvestProgress.enabled = true;
			
			if(obj.GetComponent<StructureStateManager>().GetPeek().ToString().Equals("AIStateStructureCreation"))
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.opacity = 0.0f;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.enabled = true;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.fadeTo(1.0f, 0.2f);
				//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.opacity = 0.0f;
				//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.enabled = true;
				//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.fadeTo(1.0f, 0.2f);
				//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblBuildTimer.opacity = 0.0f;
				//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblBuildTimer.enabled = true;
				//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblBuildTimer.fadeTo(1.0f, 0.2f);
				
				GameObject.Find("3-NotificationInstaBuildConfirm Button").GetComponent<InstaBuildAction>().obj = obj;
				GameObject.Find("7-BuildTimer Image").GetComponent<BuildTimerScript>().obj = obj;
			}
			else if(obj.GetComponent<StructureStateManager>().GetPeek().ToString().Equals("AIStateStructureOperational") && upgradable)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.opacity = 0.0f;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled = true;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.fadeTo(1.0f, 0.2f);
				
				GameObject.Find("3-NotificationUpgradeConfirm Button").GetComponent<UpgradeStructureAction>().obj = obj;
			}
			
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.fadeTo(1.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.fadeTo(1.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.fadeTo(1.0f, 0.2f);
			
			GameObject.Find("3-Harvest Button").GetComponent<HarvestAction>().obj = obj;
			GameObject.Find("6-ReplaceStructure Button").GetComponent<ReplaceStructureAction>().obj = obj;
			GameObject.Find("3-NotificationDestroyConfirm Button").GetComponent<DestroyStructureAction>().obj = obj;
			GameObject.Find("10-StructureHealthFill Image").GetComponent<UpdateStructureHealthFillScript>().obj = obj;
			GameObject.Find("13-HarvestProgressFill Image").GetComponent<UpdateHarvestProgressFillScript>().obj = obj;
			
		}
		else if(structureType == "Playerhouse")
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.fadeTo(1.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.fadeTo(1.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgStructureHealthFill.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureHealth.enabled = true;
				
			if(obj.GetComponent<Level>().GetLevel() < 10)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.opacity = 0.0f;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled = true;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.fadeTo(1.0f, 0.2f);
			
				GameObject.Find("3-NotificationUpgradeConfirm Button").GetComponent<UpgradeStructureAction>().obj = obj;
			}
			
			GameObject.Find("10-StructureHealthFill Image").GetComponent<UpdateStructureHealthFillScript>().obj = obj;
			
		}
		else if(structureType == "None")
		{
			
		}
		else
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.fadeTo(1.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgSubMenuBG.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.fadeTo(1.0f, 0.2f);			
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.enabled = true;
			
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.fadeTo(1.0f, 0.2f);
			
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.opacity = 0.0f;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnReplaceStructure.fadeTo(1.0f, 0.2f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgStructureHealthFill.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureHealth.enabled = true;
			
			if(obj.GetComponent<StructureStateManager>().GetPeek().ToString().Equals("AIStateStructureCreation"))
			{
				
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.opacity = 0.0f;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.enabled = true;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnInstaBuild.fadeTo(1.0f, 0.2f);
				
				GameObject.Find("3-NotificationInstaBuildConfirm Button").GetComponent<InstaBuildAction>().obj = obj;
			}
			else if(obj.GetComponent<StructureStateManager>().GetPeek().ToString().Equals("AIStateStructureOperational") && upgradable)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.opacity = 0.0f;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.enabled = true;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUpgradeStructure.fadeTo(1.0f, 0.2f);
				
				GameObject.Find("3-NotificationUpgradeConfirm Button").GetComponent<UpgradeStructureAction>().obj = obj;
			}
			
			GameObject.Find("6-ReplaceStructure Button").GetComponent<ReplaceStructureAction>().obj = obj;
			GameObject.Find("3-NotificationDestroyConfirm Button").GetComponent<DestroyStructureAction>().obj = obj;
			GameObject.Find("10-StructureHealthFill Image").GetComponent<UpdateStructureHealthFillScript>().obj = obj;
		}
	}
}
