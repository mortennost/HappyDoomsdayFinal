using UnityEngine;
using System.Collections;

public class UpdateStructureHealthFillScript : MonoBehaviour 
{
	
	[HideInInspector]
	public GameObject obj;
	
	float maxWidth = 385.0f;
	
	int currentHealth;
	int maxHealth;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(obj != null)
		{
			currentHealth = obj.GetComponent<Health>().CurHealth;
			maxHealth = obj.GetComponent<Health>().MaxHealth;
		
			if(currentHealth <= 0)
			{
				currentHealth = 0;
			}
			
			if(obj.GetComponent<StructureScript>().isBuilding)
			{
				AIStateStructureCreation temp = obj.GetComponent<StructureStateManager>().GetPeek() as AIStateStructureCreation;
			
				float buildTimerMax = obj.GetComponent<StructureScript>().BuildTime;
				float buildTimerCurrent = temp._buildTimer;
				float currentPercent = buildTimerCurrent / buildTimerMax;
				
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.label.text = (int)(currentPercent * 100) + "%";	
			}
			else
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureLevel.label.text = "Lv. " + obj.GetComponent<Level>().GetLevel();
			}
			
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgStructureHealthFill.setWidth(((float)currentHealth / (float)maxHealth) * maxWidth);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblStructureHealth.label.text = "Health: " + currentHealth + " / " + maxHealth;			
		}
	}
}
