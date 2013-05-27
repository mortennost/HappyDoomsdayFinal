using UnityEngine;
using System.Collections;

public class DangerScreenEffectScript : MonoBehaviour {
	
	float elapsed;
	float maxFlashTime;
	float flashCooldown;
	bool flashOnCooldown;
	float flashCooldownTimer;
	bool messageShown;

	// Use this for initialization
	void Start ()
	{
		elapsed = 0.0f;
		maxFlashTime = 3.0f;
		flashCooldown = 5.0f;
		flashOnCooldown = false;
		flashCooldownTimer = 0.0f;
		messageShown = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameObject.FindGameObjectWithTag("Enemy") != null)
		{
			if(GameObject.Find("Workshed(Clone)") != null)
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelTraps.enabled = true;
			}
			else
			{
				if(!messageShown)
				{
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotification.GetComponent<NotificationScript>().ShowMessage("No Workshed: \nBuild a Workshed to access Traps");
					messageShown = true;
				}
			}
			
			elapsed += Time.deltaTime;
			
			if(elapsed >= maxFlashTime)
			{
				flashOnCooldown = true;
			}
			
			if(!flashOnCooldown)
			{
				Activate();
				float newOpacity = (Mathf.Sin( elapsed * 5.0f ) + 1.0f ) / 2.0f;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgDangerEffect.setOpacity(newOpacity);
			}
			else
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgDangerEffect.setOpacity(0.0f);
				Deactivate();
				
				flashCooldownTimer += Time.deltaTime;
				
				if(flashCooldownTimer >= flashCooldown)
				{
					flashCooldownTimer = 0.0f;
					elapsed = 0.0f;
					flashOnCooldown = false;
				}
			}
		}
		else
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelTraps.enabled = false;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgDangerEffect.setOpacity(0.0f);
			Deactivate();
			messageShown = false;
		}
	}
	
	void Activate()
	{
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgDangerEffect.enabled = true;
	}
	
	void Deactivate()
	{
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._imgDangerEffect.enabled = false;
	}
}
