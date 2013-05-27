using UnityEngine;
using System.Collections;

public class NotificationScript : MonoBehaviour {
	
	//float timeDisplayed = 0.0f;
	//string message;
	//float timeToDisplay = 3.0f;
	//bool displayMessage = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		if(displayMessage)
		{
			UpdateMessage();
		}
		*/
	}
	
	public void ShowMessage(string text)
	{
		/*
		displayMessage = true;
		message = text;
		*/
		
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelNotificationInfo.enabled = true;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotificationInfoMessage.label.text = "  " + text;
	}
	
	public void ShowMessage(string header, string text)
	{
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelNotificationInfo.enabled = true;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotificationInfoHeader.label.text = "  " + header;
		GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotificationInfoMessage.label.text = "  " + text;
	}
	
	/*
	public void UpdateMessage()
	{
		if(timeDisplayed < timeToDisplay)
		{
			timeDisplayed += Time.deltaTime;
			//Debug.Log(timeDisplayed);
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelNotificationInfo.enabled = true;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotificationInfoMessage.label.text = "  " + message;
		}
		else
		{
			displayMessage = false;
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelNotificationInfo.enabled = false;
			timeDisplayed = 0.0f;
		}
	}
	*/
}
