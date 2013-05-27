using UnityEngine;
using System.Collections;

public class BuildTimerScript : MonoBehaviour 
{
	public Texture2D buildTimer0;
	public Texture2D buildTimer25;
	public Texture2D buildTimer50;
	public Texture2D buildTimer75;
	public Texture2D buildTimer100;
	
	Texture2D currentTexture;
	float buildTimerMax;
	float buildTimerCurrent;
	
	[HideInInspector]
	public GameObject obj;
	[HideInInspector]
	public float currentPercent;
	

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(obj != null && obj.GetComponent<StructureScript>().isBuilding)
		{
			AIStateStructureCreation temp = obj.GetComponent<StructureStateManager>().GetPeek() as AIStateStructureCreation;
			
			buildTimerMax = obj.GetComponent<StructureScript>().BuildTime;
			buildTimerCurrent = temp._buildTimer;
			currentPercent = buildTimerCurrent / buildTimerMax;
			
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblBuildTimer.label.text = "" + (int)(currentPercent * 100) + "%";
			
			if(currentPercent < 0.25f && currentPercent >= 0.0f)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.image = buildTimer0;
			}
			else if(currentPercent >= 0.25f && currentPercent < 0.5f)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.image = buildTimer25;
			}
			else if(currentPercent >= 0.5f && currentPercent < 0.75f)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.image = buildTimer50;
			}
			else if(currentPercent >= 0.75f && currentPercent < 1.0f)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.image = buildTimer75;
			}
			else if(currentPercent >= 1.0f)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.image = buildTimer100;
			}
		}
		else if(obj != null && !obj.GetComponent<StructureScript>().isBuilding)
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.fadeTo(0.0f, 1.5f);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblBuildTimer.fadeTo(0.0f, 1.5f);
			
			StartCoroutine("WaitAndDisable");			
		}
	}
	
	IEnumerator WaitAndDisable()
	{
		
		yield return new WaitForSeconds(1.5f);
		
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgBuildTimer.enabled = false;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblBuildTimer.enabled = false;
	}
}
