using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.GameCenter;

public class ResourceManagerScript : MonoBehaviour 
{
	int _id;
	
	public int _foodCount;
	public int _maxFoodCount;
	int _baseMaxFood;
	float _maxFoodModifier;
	
	public int _waterCount;
	public int _maxWaterCount;
	int _baseMaxWater;
	float _maxWaterModifier;
	
	public int _scrapCount;
	public int _maxScrapCount;
	
	public int _workerCount;
	public int _maxWorkerCount;
	public int _hiredWorkerCount;
	public int _maxHiredWorkerCount;
	
	public int _currentExperience;
	public int _maxExperience;
	public int _currentLevel;
	public int _maxLevel;
	public int _accumulatedExperience;
	
	public int ID {
		get { return _id; }
		set { _id = value; }
	}
	
	public int _currentHarvesters = 0;
	public int _maxHarvesters;
	public int _currentTurrets = 0;
	public int _maxTurrets;
	
	[HideInInspector]
	public int _foodConvert;
	[HideInInspector]
	public int _waterConvert;
	[HideInInspector]
	public int _scrapCostConvert;
	
	void Awake()
	{
		_foodCount = 1000;
		_waterCount = 1000;
		_scrapCount = 100;
		_workerCount = 0;
		_currentExperience = 0;
		_currentLevel = 1;
	}
	
	// Use this for initialization
	void Start () 
	{
		
		_baseMaxFood = 3000;
		_baseMaxWater = 3000;
		_maxScrapCount = 500;
		
		_maxFoodModifier = 1.8f;
		_maxWaterModifier = 1.8f;
		
		_maxWorkerCount = 2;
		
		_maxExperience = 500;
		_maxLevel = 10;
		_accumulatedExperience = 0;
		
		_foodConvert = 0;
		_waterConvert = 0;
		_scrapCostConvert = 0;
		
		_maxHarvesters = 2;
		_maxTurrets = 4;
		
		UpdateResources();
		//Time.timeScale = 10.0f;
	}
	
	public void UpdateResources()
	{
		int lvl = GameObject.Find("Playerhouse").GetComponent<Level>().GetLevel() - 1;
		
		_maxFoodCount = ( int )( _baseMaxFood * Mathf.Pow ( _maxFoodModifier, lvl ) );
		_maxWaterCount = ( int )( _baseMaxWater * Mathf.Pow ( _maxWaterModifier, lvl ) );
	}
	
	public void AddFood(int food)
	{
		if(_foodCount < _maxFoodCount)
		{
			if(_foodCount + food > _maxFoodCount)
			{
				_foodCount = _maxFoodCount;
			}
			else
			{
				_foodCount += food;
			}
		}
	}
	
	public void DepleteFood(int food)
	{
		_foodCount -= food;
	}
	
	public void SetMaxFoodCount(int maxFood)
	{
		_maxFoodCount = maxFood;
	}
	
	public int GetFood()
	{
		return _foodCount;
	}
	
	public int GetMaxFood()
	{
		return _maxFoodCount;
	}
	
	public void AddWater(int water)
	{
		if(_waterCount < _maxWaterCount)
		{
			if(_waterCount + water > _maxWaterCount)
			{
				_waterCount = _maxWaterCount;
			}
			else
			{
				_waterCount += water;
			}
		}
	}
	
	public void DepleteWater(int water)
	{
		_waterCount -= water;
	}
	
	public void SetMaxWaterCount(int maxWater)
	{
		_maxWaterCount = maxWater;
	}
	
	public int GetWater()
	{
		return _waterCount;
	}
	
	public int GetMaxWater()
	{
		return _maxWaterCount;
	}
	
	public void AddScrap(int scrap)
	{
		/*
		if(_scrapCount < _maxScrapCount)
		{
			if(_scrapCount + scrap > _maxScrapCount)
			{
				_scrapCount = _maxScrapCount;
			}
			else
			{
				_scrapCount += scrap;
			}
		}
		*/
		_scrapCount += scrap;
	}
	
	public void DepleteScrap(int scrap)
	{
		_scrapCount -= scrap;
	}
	
	public void SetMaxScrapCount(int maxScrap)
	{
		_maxScrapCount = maxScrap;
	}
	
	public int GetScrap()
	{
		return _scrapCount;
	}
	
	public int GetMaxScrap()
	{
		return _maxScrapCount;
	}
	
	public void AddWorker()
	{
		_workerCount++;
	}
	
	public void RemoveWorker()
	{
		if(_workerCount > 0)
		{
			_workerCount--;
		}
		if(_hiredWorkerCount > 0)
		{
			RemoveHiredWorker();
		}
	}
	
	public void AddHiredWorker()
	{
		_hiredWorkerCount++;
		_maxHiredWorkerCount++;
		_maxWorkerCount += _maxHiredWorkerCount;
	}
	
	public void RemoveHiredWorker()
	{
		_maxHiredWorkerCount--;
		_hiredWorkerCount--;
		_maxWorkerCount--;
	}
	
	public int GetHiredWorkerCount()
	{
		return _hiredWorkerCount;
	}
	
	public void SetMaxWorkerCount(int maxWorkers)
	{
		_maxWorkerCount = maxWorkers;
	}
	
	public int GetWorkerCount()
	{
		return _workerCount;
	}
	
	public int GetMaxWorkerCount()
	{
		return _maxWorkerCount;
	}
	
	public void AddExperience(int experience)
	{
		_currentExperience += experience;
		
		if(_currentExperience >= _maxExperience)
		{
			int expOverflow = _currentExperience - _maxExperience;
			
			AddLevel();
			
			_currentExperience = expOverflow;
		}
		
#if UNITY_IPHONE
		_accumulatedExperience += _currentExperience;
		
		Social.ReportScore(_accumulatedExperience, "1", (result) => 
		{
			Debug.Log (result ? "Reported score successfully!" : "Failed to report score");
		});
#endif
		
	}
	
	public int GetExperience()
	{
		return _currentExperience;
	}
	
	public int GetMaxExperience()
	{
		return _maxExperience;
	}
	
	public void ResetExperience()
	{
		_currentLevel = 1;
		_currentExperience = 0;
		_maxExperience = 500;
	}
	
	public void AddLevel()
	{
		if(_currentLevel < _maxLevel)
		{
			_currentLevel++;
			_maxExperience *= 2; 
		}
	}
	
	public int GetLevel()
	{
		return _currentLevel;
	}
	
	public int GetMaxLevel()
	{
		return _maxLevel;
	}
	
	public int GetCompoundLevel()
	{
		GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
		
		if (GameObject.FindGameObjectWithTag("Structure") != null)
		{
			float levelAcc = GameObject.Find("Playerhouse").GetComponent<Level>().GetLevel(); // Accumulated levels
			float structureCount = 1; // Total structures
			
			foreach(GameObject str in structures)
			{
				levelAcc += str.GetComponent<Level>().GetLevel();
				
				structureCount++;
			}
			
			//print ("Level: " + levelAdded + " Count: " + structureCount);
			float averageLevel = Mathf.Round(levelAcc / structureCount);
			//print("Avg: " + levelAdded / structureCount + " Rounded: " + averageLevel);
			
			return (int)averageLevel;
		}
		else
		{
			GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._lblXPLevel.label.text = "1";
			
			return 1;
		}
	}
	
	public void ResetScrap()
	{
		_scrapCount = 0;
	}
	
	public void ResetResources()
	{
		_foodCount = 0;
		_waterCount = 0;
	}
}
