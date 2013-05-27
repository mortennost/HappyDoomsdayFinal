using UnityEngine;
using System.Collections;
using iGUI;

public class iGUICode_GUI_mockup2 : MonoBehaviour
{
	[HideInInspector]
	public iGUIRoot root;
	[HideInInspector]
	public iGUIImage _imgDangerEffect;
	[HideInInspector]
	public iGUILabel _lblFPSCounter;
	
	#region HUD
	[HideInInspector]
	public iGUIContainer _containerHUD;
	[HideInInspector]
	public iGUIPanel _panelCredits;
	
	[HideInInspector]
	public iGUIPanel _panelNotificationInstaBuild;
	[HideInInspector]
	public iGUILabel _lblNotificationInstaBuildHeader;
	[HideInInspector]
	public iGUILabel _lblNotificationInstaBuildMessage;
	[HideInInspector]
	public iGUIButton _btnNotificationInstaBuildConfirm;
	[HideInInspector]
	public iGUIButton _btnNotificationInstaBuildDecline;
	[HideInInspector]
	public iGUILabel _lblNotificationInstaBuildResourceCost;
	
	[HideInInspector]
	public iGUIPanel _panelNotificationUpgrade;
	[HideInInspector]
	public iGUILabel _lblNotificationUpgradeHeader;
	[HideInInspector]
	public iGUILabel _lblNotificationUpgradeMessage;
	[HideInInspector]
	public iGUIButton _btnNotificationUpgradeConfirm;
	[HideInInspector]
	public iGUIButton _btnNotificationUpgradeDecline;
	[HideInInspector]
	public iGUILabel _lblNotificationUpgradeFoodCost;
	[HideInInspector]
	public iGUILabel _lblNotificationUpgradeWaterCost;
	
	[HideInInspector]
	public iGUIPanel _panelNotificationDestroy;
	[HideInInspector]
	public iGUILabel _lblNotificationDestroyHeader;
	[HideInInspector]
	public iGUILabel _lblNotificationDestroyMessage;
	[HideInInspector]
	public iGUIButton _btnNotificationDestroyConfirm;
	[HideInInspector]
	public iGUIButton _btnNotificationDestroyDecline;
	[HideInInspector]
	public iGUILabel _lblNotificationDestroyResourceCost;
	
	[HideInInspector]
	public iGUIPanel _panelNotificationInfo;
	[HideInInspector]
	public iGUILabel _lblNotificationInfoHeader;
	[HideInInspector]
	public iGUILabel _lblNotificationInfoMessage;
	[HideInInspector]
	public iGUIButton _btnNotificationInfoOK;
	
	[HideInInspector]
	public iGUIButton _btnOptions;
	[HideInInspector]
	public iGUIButton _btnOpenShop;
	[HideInInspector]
	public iGUIButton _btnPlaceBuilding;
	[HideInInspector]
	public iGUIButton _btnPlaceTrap;
	[HideInInspector]
	public iGUIButton _btnCancelBuilding;
	[HideInInspector]
	public iGUIButton _btnCancelTrap;
	[HideInInspector]
	public iGUIButton _btnDestroyStructure;
	[HideInInspector]
	public iGUIButton _btnHarvest;
	[HideInInspector]
	public iGUIButton _btnInstaBuild;
	[HideInInspector]
	public iGUIButton _btnUpgradeStructure;
	[HideInInspector]
	public iGUIButton _btnReplaceStructure;
	[HideInInspector]
	public iGUIButton _btnSpawnCreep;
	[HideInInspector]
	public iGUIButton _btnToggleInvasion;
	[HideInInspector]
	public iGUILabel _lblNotification;
	[HideInInspector]
	public iGUILabel _lblInvasionTimer;
	[HideInInspector]
	public iGUIImage _imgBuildTimer;
	[HideInInspector]
	public iGUILabel _lblBuildTimer;
	[HideInInspector]
	public iGUIContainer _containerSubMenu;
	[HideInInspector]
	public iGUIImage _imgSubMenuBG;
	[HideInInspector]
	public iGUILabel _lblStructureLevel;
	[HideInInspector]
	public iGUIImage _imgStructureHealthFill;
	[HideInInspector]
	public iGUILabel _lblStructureHealth;
	[HideInInspector]
	public iGUIImage _imgHarvestProgressBG;
	[HideInInspector]
	public iGUIImage _imgHarvestProgressFill;
	[HideInInspector]
	public iGUILabel _lblHarvestProgress;
	
	#region XP-Bar
	[HideInInspector]
	public iGUIContainer _containerXP;
	[HideInInspector]
	public iGUIImage _imgXPBackground;
	[HideInInspector]
	public iGUIImage _imgXPForeground;
	[HideInInspector]
	public iGUIContainer _containerFillColor;
	[HideInInspector]
	public iGUIImage _imgXPFillColor;
	[HideInInspector]
	public iGUILabel _lblXPLevel;
	[HideInInspector]
	public iGUIImage _imgWorkersPlank;
	#endregion
	
	#region ResourceLabels
	[HideInInspector]
	public iGUIContainer _containerResources;
	[HideInInspector]
	public iGUILabel _lblFood;
	[HideInInspector]
	public iGUILabel _lblWater;
	[HideInInspector]
	public iGUILabel _lblScrap;
	[HideInInspector]
	public iGUILabel _lblWorkers;
	[HideInInspector]
	public iGUIImage _imgFoodIcon;
	[HideInInspector]
	public iGUIImage _imgWaterIcon;
	[HideInInspector]
	public iGUIImage _imgScrapIcon;
	[HideInInspector]
	public iGUIImage _imgFoodPlank;
	[HideInInspector]
	public iGUIImage _imgWaterPlank;
	[HideInInspector]
	public iGUIImage _imgScrapPlank;
	[HideInInspector]
	public iGUIImage _imgFoodFill;
	[HideInInspector]
	public iGUIImage _imgWaterFill;
	[HideInInspector]
	public iGUIImage _imgScrapFill;
	#endregion
	
	#region TrapPanel
	[HideInInspector]
	public iGUIPanel _panelTraps;
	[HideInInspector]
	public iGUIPanel _panelTrapsInfo;
	[HideInInspector]
	public iGUIButton _btnUseElectricTrap;
	[HideInInspector]
	public iGUIButton _btnUseMineTrap;
	[HideInInspector]
	public iGUIButton _btnUseControlTrap;
	[HideInInspector]
	public iGUILabel _lblElectric;
	[HideInInspector]
	public iGUILabel _lblMine;
	[HideInInspector]
	public iGUILabel _lblControl;
	[HideInInspector]
	public iGUIButton _btnTrapsInfo;
	[HideInInspector]
	public iGUILabel _lblTrapsInfoHeader;
	[HideInInspector]
	public iGUILabel _lblTrapsInfoMessage;
	[HideInInspector]
	public iGUIButton _btnTrapsInfoConfirm;
	#endregion
	
	#endregion
	
	#region ShopMenu
	[HideInInspector]
	public iGUIPanel _panelShop;
	[HideInInspector]
	public iGUIImage _imgShopBackground;
	[HideInInspector]
	public iGUIButton _btnCloseShop;
	
	#region CategoryView
	[HideInInspector]
	public iGUIContainer _containerShopCategories;
	[HideInInspector]
	public iGUIButton _btnTurretsCategory;
	[HideInInspector]
	public iGUIButton _btnHarvestersCategory;
	[HideInInspector]
	public iGUIButton _btnUtilityCategory;
	[HideInInspector]
	public iGUIButton _btnRealMoneyCategory;
	#endregion
	
	#region TurretsCategory
	[HideInInspector]
	public iGUIScrollView _scrlTurrets;
	[HideInInspector]
	public iGUIButton _btnCrossbowTurret;
	[HideInInspector]
	public iGUIButton _btnFlamethrowerTurret;
	[HideInInspector]
	public iGUIButton _btnFreezerTurret;
	[HideInInspector]
	public iGUIButton _btnScarecrowTurret;
	
	[HideInInspector]
	public iGUIContainer _containerCrossbowBtn;
	[HideInInspector]
	public iGUILabel _lblCrossbowHeader;
	[HideInInspector]
	public iGUILabel _lblCrossbowDescription;
	[HideInInspector]
	public iGUILabel _lblCrossbowFoodCost;
	[HideInInspector]
	public iGUIImage _imgCrossbowFoodCost;
	[HideInInspector]
	public iGUILabel _lblCrossbowWaterCost;
	[HideInInspector]
	public iGUIImage _imgCrossbowWaterCost;
	[HideInInspector]
	public iGUILabel _lblCrossbowBuildTime;
	[HideInInspector]
	public iGUIImage _imgCrossbowBuildTime;
	
	[HideInInspector]
	public iGUIContainer _containerFlamethrowerBtn;
	[HideInInspector]
	public iGUILabel _lblFlamethrowerHeader;
	[HideInInspector]
	public iGUILabel _lblFlamethrowerDescription;
	[HideInInspector]
	public iGUILabel _lblFlamethrowerFoodCost;
	[HideInInspector]
	public iGUIImage _imgFlamethrowerFoodCost;
	[HideInInspector]
	public iGUILabel _lblFlamethrowerWaterCost;
	[HideInInspector]
	public iGUIImage _imgFlamethrowerWaterCost;
	[HideInInspector]
	public iGUILabel _lblFlamethrowerBuildTime;
	[HideInInspector]
	public iGUIImage _imgFlamethrowerBuildTime;
	
	[HideInInspector]
	public iGUIContainer _containerFreezerBtn;
	[HideInInspector]
	public iGUILabel _lblFreezerHeader;
	[HideInInspector]
	public iGUILabel _lblFreezerDescription;
	[HideInInspector]
	public iGUILabel _lblFreezerFoodCost;
	[HideInInspector]
	public iGUIImage _imgFreezerFoodCost;
	[HideInInspector]
	public iGUILabel _lblFreezerWaterCost;
	[HideInInspector]
	public iGUIImage _imgFreezerWaterCost;
	[HideInInspector]
	public iGUILabel _lblFreezerBuildTime;
	[HideInInspector]
	public iGUIImage _imgFreezerBuildTime;
	
	[HideInInspector]
	public iGUIContainer _containerTauntBtn;
	[HideInInspector]
	public iGUILabel _lblTauntHeader;
	[HideInInspector]
	public iGUILabel _lblTauntDescription;
	[HideInInspector]
	public iGUILabel _lblTauntFoodCost;
	[HideInInspector]
	public iGUIImage _imgTauntFoodCost;
	[HideInInspector]
	public iGUILabel _lblTauntWaterCost;
	[HideInInspector]
	public iGUIImage _imgTauntWaterCost;
	[HideInInspector]
	public iGUILabel _lblTauntBuildTime;
	[HideInInspector]
	public iGUIImage _imgTauntBuildTime;
	#endregion
	
	#region HarvestersCategory
	[HideInInspector]
	public iGUIScrollView _scrlHarvesters;
	[HideInInspector]
	public iGUIButton _btnWellHarvester;
	[HideInInspector]
	public iGUIButton _btnRainHarvester;
	[HideInInspector]
	public iGUIButton _btnChickenHarvester;
	[HideInInspector]
	public iGUIButton _btnFieldHarvester;
	
	[HideInInspector]
	public iGUIContainer _containerWellBtn;
	[HideInInspector]
	public iGUILabel _lblWellHeader;
	[HideInInspector]
	public iGUILabel _lblWellDescription;
	[HideInInspector]
	public iGUILabel _lblWellFoodCost;
	[HideInInspector]
	public iGUIImage _imgWellFoodCost;
	[HideInInspector]
	public iGUILabel _lblWellWaterCost;
	[HideInInspector]
	public iGUIImage _imgWellWaterCost;
	[HideInInspector]
	public iGUILabel _lblWellBuildTime;
	[HideInInspector]
	public iGUIImage _imgWellBuildTime;
	
	[HideInInspector]
	public iGUIContainer _containerRainBtn;
	[HideInInspector]
	public iGUILabel _lblRainHeader;
	[HideInInspector]
	public iGUILabel _lblRainDescription;
	[HideInInspector]
	public iGUILabel _lblRainFoodCost;
	[HideInInspector]
	public iGUIImage _imgRainFoodCost;
	[HideInInspector]
	public iGUILabel _lblRainWaterCost;
	[HideInInspector]
	public iGUIImage _imgRainWaterCost;
	[HideInInspector]
	public iGUILabel _lblRainBuildTime;
	[HideInInspector]
	public iGUIImage _imgRainBuildTime;
	
	[HideInInspector]
	public iGUIContainer _containerChickenBtn;
	[HideInInspector]
	public iGUILabel _lblChickenHeader;
	[HideInInspector]
	public iGUILabel _lblChickenDescription;
	[HideInInspector]
	public iGUILabel _lblChickenFoodCost;
	[HideInInspector]
	public iGUIImage _imgChickenFoodCost;
	[HideInInspector]
	public iGUILabel _lblChickenWaterCost;
	[HideInInspector]
	public iGUIImage _imgChickenWaterCost;
	[HideInInspector]
	public iGUILabel _lblChickenBuildTime;
	[HideInInspector]
	public iGUIImage _imgChickenBuildTime;
	
	[HideInInspector]
	public iGUIContainer _containerFieldBtn;
	[HideInInspector]
	public iGUILabel _lblFieldHeader;
	[HideInInspector]
	public iGUILabel _lblFieldDescription;
	[HideInInspector]
	public iGUILabel _lblFieldFoodCost;
	[HideInInspector]
	public iGUIImage _imgFieldFoodCost;
	[HideInInspector]
	public iGUILabel _lblFieldWaterCost;
	[HideInInspector]
	public iGUIImage _imgFieldWaterCost;
	[HideInInspector]
	public iGUILabel _lblFieldBuildTime;
	[HideInInspector]
	public iGUIImage _imgFieldBuildTime;
	#endregion
	
	#region UtilityCategory
	[HideInInspector]
	public iGUIScrollView _scrlUtility;
	[HideInInspector]
	public iGUIButton _btnWorkshedUtility;
	[HideInInspector]
	public iGUIButton _btnWallUtility;
	
	[HideInInspector]
	public iGUIContainer _containerWorkshedBtn;
	[HideInInspector]
	public iGUILabel _lblWorkshedHeader;
	[HideInInspector]
	public iGUILabel _lblWorkshedDescription;
	[HideInInspector]
	public iGUILabel _lblWorkshedFoodCost;
	[HideInInspector]
	public iGUIImage _imgWorkshedFoodCost;
	[HideInInspector]
	public iGUILabel _lblWorkshedWaterCost;
	[HideInInspector]
	public iGUIImage _imgWorkshedWaterCost;
	[HideInInspector]
	public iGUILabel _lblWorkshedBuildTime;
	[HideInInspector]
	public iGUIImage _imgWorkshedBuildTime;
	
	[HideInInspector]
	public iGUIContainer _containerWallBtn;
	[HideInInspector]
	public iGUILabel _lblWallHeader;
	[HideInInspector]
	public iGUILabel _lblWallDescription;
	[HideInInspector]
	public iGUILabel _lblWallFoodCost;
	[HideInInspector]
	public iGUIImage _imgWallFoodCost;
	[HideInInspector]
	public iGUILabel _lblWallWaterCost;
	[HideInInspector]
	public iGUIImage _imgWallWaterCost;
	[HideInInspector]
	public iGUILabel _lblWallBuildTime;
	[HideInInspector]
	public iGUIImage _imgWallBuildTime;
	#endregion
	
	#endregion
	
	#region OptionsMenu
	[HideInInspector]
	public iGUIPanel _panelOptions;
	[HideInInspector]
	public iGUIButton _btnExitOptions;
	[HideInInspector]
	public iGUILabel _lblOptionsHeader;
	[HideInInspector]
	public iGUIIntHorizontalSlider _sliderVolume;
	[HideInInspector]
	public iGUIButton _btnCredits;
	[HideInInspector]
	public iGUIButton _btnToggleFPS;
	#endregion
	
	#region IAPMenu
	[HideInInspector]
	public iGUIPanel _panelIAP;
	[HideInInspector]
	public iGUIButton _btnExitIAP;
	[HideInInspector]
	public iGUILabel _lblIAPHeader;
	[HideInInspector]
	public iGUIButton _btnConvertScrapMenu;
	[HideInInspector]
	public iGUIButton _btnHireWorker;
	[HideInInspector]
	public iGUIButton _btnIAPBundle1;
	[HideInInspector]
	public iGUIButton _btnIAPBundle2;
	[HideInInspector]
	public iGUIButton _btnIAPBundle3;
	[HideInInspector]
	public iGUIButton _btnIAPBundle4;
	[HideInInspector]
	public iGUILabel _lblIAPBundle1;
	[HideInInspector]
	public iGUILabel _lblIAPBundle2;
	[HideInInspector]
	public iGUILabel _lblIAPBundle3;
	[HideInInspector]
	public iGUILabel _lblIAPBundle4;
	#endregion
	
	#region ConvertScrapMenu
	[HideInInspector]
	public iGUIPanel _panelConvertScrap;
	[HideInInspector]
	public iGUIButton _btnExitConvertScrap;
	[HideInInspector]
	public iGUILabel _lblConvertScrapHeader;
	[HideInInspector]
	public iGUIButton _btnDecreaseConvert;
	[HideInInspector]
	public iGUIButton _btnIncreaseConvert;
	[HideInInspector]
	public iGUILabel _lblFoodWaterConvert;
	[HideInInspector]
	public iGUILabel _lblScrapCostConvert;
	[HideInInspector]
	public iGUIButton _btnConvertScrap;
	#endregion
	
	#region GodModeMenu
	[HideInInspector]
	public iGUIPanel _panelGodMode;
	[HideInInspector]
	public iGUIButton _btnExitGodMode;
	[HideInInspector]
	public iGUILabel _lblGodModeHeader;
	[HideInInspector]
	public iGUIButton _btnAddScrap;
	[HideInInspector]
	public iGUIButton _btnResetScrap;
	[HideInInspector]
	public iGUIButton _btnAddResources;
	[HideInInspector]
	public iGUIButton _btnResetResources;
	[HideInInspector]
	public iGUIButton _btnAddExperience;
	[HideInInspector]
	public iGUIButton _btnResetExperience;
	#endregion

	static iGUICode_GUI_mockup2 instance;
	
	void Awake()
	{
		instance=this;
	}
	public static iGUICode_GUI_mockup2 getInstance()
	{
		return instance;
	}
	public void _containerSubMenu_MouseOver(iGUIContainer caller){
		
	}

}