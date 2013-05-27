using UnityEngine;
using System.Collections;

public class PlayerhouseScript : MonoBehaviour 
{
	[HideInInspector]
	public bool destroyed;
	float repairElapsed;
	float repairTime;
	
	int level;
	
	// Use this for initialization
	void Start () 
	{		
		destroyed = false;
		repairElapsed = 0.0f;
		repairTime = 0.2f * 60.0f;
		
		level = gameObject.GetComponent<Level>().GetLevel();
		
		// Set GridSnapper size
		GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().xSize = GetComponent<StructureScript>().xSize;
		GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().zSize = GetComponent<StructureScript>().zSize;
		
		GameObject.Find("Gridsnapper").transform.position = GameObject.Find("Grid").GetComponent<GridScript>().GetCenter();
		GameObject.Find("Gridsnapper").GetComponent<GridsnapperScript>().Snap();
		
		transform.position = GameObject.Find("Gridsnapper").transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.GetComponent<Level>().GetLevel() != level)
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().UpdateResources();
			level = gameObject.GetComponent<Level>().GetLevel();
		}
		
		if(level < 3)
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxHarvesters = 4;
		}
		else if(level >= 3 && level < 5)
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxHarvesters = 5;
		}
		else if(level >= 5 && level < 7)
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxHarvesters = 6;
		}
		else if(level >= 7 && level < 9)
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxHarvesters = 7;
		}
		else if(level >= 9)
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxHarvesters = 8;
		}
		
		switch(level)
		{
		case 1:
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets = 10;
			break;
		case 2:
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets = 11;
			break;
		case 3:
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets = 12;
			break;
		case 4:
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets = 13;
			break;
		case 5:
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets = 14;
			break;
		case 6:
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets = 15;
			break;
		case 7:
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets = 16;
			break;
		case 8:
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets = 17;
			break;
		case 9:
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets = 18;
			break;
		case 10:
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets = 19;
			break;
		}
		
		//GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxTurrets = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>()._maxHarvesters * 2;
		
		if(destroyed)
		{
			if(repairElapsed < repairTime)
			{
				repairElapsed += Time.deltaTime;
			}
			else
			{
				destroyed = false;
				gameObject.GetComponent<Health>().CurHealth = gameObject.GetComponent<Health>().MaxHealth;
			}
		}
		else
		{
			if(gameObject.GetComponent<Health>().getHealth() <= 0)
			{
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
				
				foreach(GameObject obj in enemies)
				{
					Destroy(obj);
				}
				
				destroyed = true;
				
				PrefabSoundEffects sounds = GetComponent<PrefabSoundEffects>();
				if (sounds != null)
					sounds.audioSources[1].Play();
				
				GameObject.Find("9-Notification Label").GetComponent<NotificationScript>().ShowMessage("Your compound was destroyed! \n" +
																										"The robots have gone to pillage another village.");
			}
		}
	}
}
