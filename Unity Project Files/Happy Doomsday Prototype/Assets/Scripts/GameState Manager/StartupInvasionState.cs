using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class StartupInvasionState : State
{
	int allocatedCreepScore = 0;
	
	public StartupInvasionState(GameObject gameObject)
		: base(gameObject)
	{
	}
	
	public override void OnStart()
	{
		CalculateInvasion();
		GameObject.Find("Creep Spawner").GetComponent<CreepSpawnScript>().SpawnStack();
		
		//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotification.GetComponent<NotificationScript>().ShowMessage("Calculating Invasion");
		
		GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new PlayState(GameObject.Find("GameStateManager")));
	}
	
	public override void OnPause()
	{		
		//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotification.GetComponent<NotificationScript>().ShowMessage("Invasion Calculation Paused");
	}
	
	public override void OnExecute()
	{
	}
	
	public override void OnContinue()
	{		
		//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotification.GetComponent<NotificationScript>().ShowMessage("Invasion Calculation Continued");
	}
	
	public override void OnStop()
	{		
		//GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblNotification.GetComponent<NotificationScript>().ShowMessage("Invasion Calculation Ended");
	}
	
	public void CalculateInvasion()
	{		
		Vector3 southSpawn = new Vector3(0.0f, 0.0f, 0.0f);
		Vector3 westSpawn = new Vector3(0.0f, 0.0f, 30.0f);
		Vector3 northSpawn = new Vector3(30.0f, 0.0f, 30.0f);
		Vector3 eastSpawn = new Vector3(30.0f, 0.0f, 0.0f);
		
		List<SpawnType> typesList = new List<SpawnType>();
		typesList.Add(new SpawnType("Bullfrog"));
		typesList.Add(new SpawnType("Boomer"));
		typesList.Add(new SpawnType("Cannoneer"));
		typesList.Add(new SpawnType("Jarhead"));
		typesList.Add(new SpawnType("Raptor"));
		typesList.Add(new SpawnType("VonSchnauser"));
		typesList.Add(new SpawnType("Lippschultz"));
		typesList.Add(new SpawnType("Krueger"));
		typesList.Add(new SpawnType("Gorilla"));
		typesList.Add(new SpawnType("Scorpion"));
		
		List<SpawnType> spawnList = new List<SpawnType>();
		
		int levelRequirement = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetLevel();
		int creepScore = levelRequirement + 10;
		
		// Add types to spawnList that DO meet the level requirement for spawning
		foreach(SpawnType type in typesList)
		{
			if(type.GetLevelRequirement() <= levelRequirement)
			{
				//Debug.Log(type.GetLevelRequirement());
				spawnList.Add(type);
			}
		}
			
		Stack<SpawnType> spawnStack = new Stack<SpawnType>();
		
		while(allocatedCreepScore < creepScore)
		{
			// Generate random number from 1 to 4
			int randomSpawnPosition = Random.Range(1, 5);
			Vector3 currentSpawnPosition = Vector3.zero;
			
			// Get a random spawn position
			switch(randomSpawnPosition)
			{
			case 1:
				currentSpawnPosition = southSpawn;
				break;
			case 2:
				currentSpawnPosition = westSpawn;
				break;
			case 3:
				currentSpawnPosition = northSpawn;
				break;
			case 4:
				currentSpawnPosition = eastSpawn;
				break;
			default:
				currentSpawnPosition = southSpawn;
				break;
			}
			
			int randomType = Random.Range(0, spawnList.Count);
			
			if(allocatedCreepScore + spawnList[randomType].GetCreepCost() <= creepScore)
			{
				allocatedCreepScore += spawnList[randomType].GetCreepCost();
			
				spawnList[randomType].SetPosition(currentSpawnPosition);
				//spawnList[randomType].SetLevel(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetCompoundLevel());
				spawnStack.Push(spawnList[randomType]);
			}
		}
		
		GameObject.Find("Creep Spawner").GetComponent<CreepSpawnScript>().AddSpawnStack(spawnStack);
	}	
}