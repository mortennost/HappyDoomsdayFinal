using UnityEngine;
using System.Collections;
using iGUI;

public class iGUICode_GUI_test : MonoBehaviour
{
	[HideInInspector]
	public iGUIRoot root1;
	
	#region MainGUI
	[HideInInspector]
	public iGUILabel _lblPrepScoreFill;
	[HideInInspector]
	public iGUILabel _lblPrepScore;
	
	[HideInInspector]
	public iGUIContainer _containerTrapBar;
	[HideInInspector]
	public iGUIButton _btnTrapOne;
	[HideInInspector]
	public iGUIButton _btnTrapTwo;
	[HideInInspector]
	public iGUIButton _btnTrapThree;
	
	[HideInInspector]
	public iGUIButton _btnOptions;
	[HideInInspector]
	public iGUIButton _btnShop;
	[HideInInspector]
	public iGUILabel _lblFood;
	[HideInInspector]
	public iGUILabel _lblScrap;
	[HideInInspector]
	public iGUILabel _lblWater;
	#endregion

	#region ShopMenu
	[HideInInspector]
	public iGUIPanel _panelShopMenu;
	[HideInInspector]
	public iGUIButton _btnQuitShop;
	
	[HideInInspector]
	public iGUIContainer _containerShopCategories;
	[HideInInspector]
	public iGUIButton _btnRealMoney;
	[HideInInspector]
	public iGUIButton _btnUtility;
	[HideInInspector]
	public iGUIButton _btnHarvesters;
	[HideInInspector]
	public iGUIButton _btnTurrets;
	
	[HideInInspector]
	public iGUIContainer _containerTurretMenu;
	[HideInInspector]
	public iGUIButton _btnBackTurret;
	[HideInInspector]
	public iGUIScrollView _scrollViewTurret;
	[HideInInspector]
	public iGUIButton _btnTurretOne;
	[HideInInspector]
	public iGUIButton _btnTurretTwo;
	[HideInInspector]
	public iGUIButton _btnTurretThree;
	[HideInInspector]
	public iGUIButton _btnTurretFour;
	[HideInInspector]
	public iGUIPanel _panelTurretInfo;
	[HideInInspector]
	public iGUIContainer _containerTurretInfo;
	[HideInInspector]
	public iGUIButton _btnSellTurret;
	[HideInInspector]
	public iGUIButton _btnBuyTurret;
	#endregion
	
	#region OptionsMenu
	[HideInInspector]
	public iGUIPanel _panelOptionsMenu;
	[HideInInspector]
	public iGUIButton _btnQuitOptions;
	
	
	[HideInInspector]
	public iGUIIntHorizontalSlider _sliderMasterVolume;
	[HideInInspector]
	public iGUIButton _btnCredits;
	#endregion
	
	// Other Member Variables
	public int _prepScore = 3;
	public int _prepScoreMax = 10;

	static iGUICode_GUI_test instance;
	
	void Awake()
	{
		instance=this;
	}
	
	public static iGUICode_GUI_test getInstance()
	{
		return instance;
	}
	
	void Update()
	{
		UpdatePrepScore();
	}
	
	void UpdatePrepScore()
	{
		_lblPrepScore.label.text = "Lv. " + _prepScore;
		
		_lblPrepScoreFill.rect = new Rect(_lblPrepScore.rect.x + 1, _lblPrepScore.rect.y + 1, _lblPrepScore.rect.width - 2, _lblPrepScore.rect.height - 2);
		
		_lblPrepScoreFill.rect.width = _lblPrepScoreFill.rect.width / _prepScoreMax * _prepScore;
	}
	public void container1_MouseOver(iGUIContainer caller){
		
	}

}
