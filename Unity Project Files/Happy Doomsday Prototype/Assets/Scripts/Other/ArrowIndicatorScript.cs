using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowIndicatorScript : MonoBehaviour {
	
	public GUISkin customSkin;
	public Texture2D indicator;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI()
	{
		GUISkin temp = GUI.skin;
		GUI.skin = customSkin;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies)
		{
			Vector3 enemyScreenPoint = Camera.mainCamera.WorldToScreenPoint(enemy.transform.position);
			
			if (enemyScreenPoint.x < 0 && Screen.height - enemyScreenPoint.y > 0 && Screen.height - enemyScreenPoint.y < Screen.height)
				GUI.Box(new Rect(0, Screen.height - enemyScreenPoint.y - 15, 30, 30), new GUIContent(indicator));
			
			if (enemyScreenPoint.x > Screen.width && Screen.height - enemyScreenPoint.y > 0 && Screen.height - enemyScreenPoint.y < Screen.height)
				GUI.Box(new Rect(Screen.width - 30, Screen.height - enemyScreenPoint.y - 15, 30, 30), new GUIContent(indicator));
			
			if (Screen.height - enemyScreenPoint.y < 0 && enemyScreenPoint.x > 0 && enemyScreenPoint.x < Screen.width)
				GUI.Box(new Rect(enemyScreenPoint.x, 0, 30, 30), new GUIContent(indicator));
			
			if (Screen.height - enemyScreenPoint.y > Screen.height && enemyScreenPoint.x > 0 && enemyScreenPoint.x < Screen.width)
				GUI.Box(new Rect(enemyScreenPoint.x, Screen.height - 30, 30, 30), new GUIContent(indicator));
			
			if (enemyScreenPoint.x < 0 && Screen.height - enemyScreenPoint.y < 0)
				GUI.Box(new Rect(0, 0, 30, 30), new GUIContent(indicator));
			
			if (enemyScreenPoint.x > Screen.width && Screen.height - enemyScreenPoint.y < 0)
				GUI.Box(new Rect(Screen.width - 30, 0, 30, 30), new GUIContent(indicator));
			
			if (enemyScreenPoint.x < 0 && Screen.height - enemyScreenPoint.y > Screen.height)
				GUI.Box(new Rect(0, Screen.height - 30, 30, 30), new GUIContent(indicator));
			
			if (enemyScreenPoint.x > Screen.width && Screen.height - enemyScreenPoint.y > Screen.height)
				GUI.Box(new Rect(Screen.width - 30, Screen.height - 30, 30, 30), new GUIContent(indicator));
		}
		
		GUI.skin = temp;
	}
}
