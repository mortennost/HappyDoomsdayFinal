using UnityEngine;
using System.Collections;
using iGUI;

public class iGUICode_Compound : MonoBehaviour{
	[HideInInspector]
	public iGUIContainer container1;
	[HideInInspector]
	public iGUIRoot root1;

	static iGUICode_Compound instance;
	void Awake(){
		instance=this;
	}
	public static iGUICode_Compound getInstance(){
		return instance;
	}

}
