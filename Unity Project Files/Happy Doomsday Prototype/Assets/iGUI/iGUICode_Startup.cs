using UnityEngine;
using System.Collections;
using iGUI;
using AssemblyCSharp;
using UnityEngine.SocialPlatforms.GameCenter;
using System.Collections.Generic;
using System.Xml;

public class iGUICode_Startup : MonoBehaviour{
	[HideInInspector]
	public iGUIImage image3;
	[HideInInspector]
	public iGUIImage image2;
	[HideInInspector]
	public iGUIImage image1;
	[HideInInspector]
	public iGUIButton button1;
	[HideInInspector]
	public iGUIButton button2;
	

	static iGUICode_Startup instance;
	void Awake(){
		instance=this;
	}
	public static iGUICode_Startup getInstance(){
		return instance;
	}

	public void button1_Click(iGUIButton caller)
	{
#if UNITY_IPHONE
		if (GameCenterSingleton.Instance.Authenticated)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
			Social.ReportProgress("1_Enter", 100, (result) => 
			{
				Debug.Log (result ? "Reported achievement #1" : "Failed to report achievement #1");	
			});
			
			image1.setEnabled(true);
			Application.LoadLevel("CompoundNewGUI");
		}
#endif
		
#if UNITY_WEBPLAYER
		image1.setEnabled(true);
		Application.LoadLevel("CompoundNewGUI");
#endif
	}

	public void button2_Click(iGUIButton caller)
	{
#if UNITY_IPHONE
		XmlWriterSettings settings = new XmlWriterSettings();
		settings.Indent = true;
		
		XmlWriter writer = XmlWriter.Create(Application.persistentDataPath + "/save.xml", settings);
		
		writer.WriteStartDocument();
			writer.WriteStartElement("savegame");
		writer.WriteEndDocument();
		
		writer.Flush();
		writer.Close();
		

		if (GameCenterSingleton.Instance.Authenticated)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
			Social.ReportProgress("1_Enter", 100, (result) => 
			{
				Debug.Log (result ? "Reported achievement #1" : "Failed to report achievement #1");	
			});
			
			image1.setEnabled(true);
			Application.LoadLevel("Tutorial");
		}
#endif
		
#if UNITY_WEBPLAYER
		image1.setEnabled(true);
		Application.LoadLevel("Tutorial");
#endif
	}

	public void image1_Click(iGUIImage caller){
		
	}

}
