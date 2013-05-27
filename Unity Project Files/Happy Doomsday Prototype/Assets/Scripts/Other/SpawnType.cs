using UnityEngine;
using System.Collections;
namespace AssemblyCSharp
{
	public class SpawnType 
	{
		string _objType;
		Vector3 _position;
		int _levelRequirement;
		int _creepCost;
		int _level;
		
		public SpawnType(string objType)
		{
			_objType = objType;
		}
		
		public void Instantiate()
		{
			
		}
		
		public void SetPosition(Vector3 position)
		{
			_position = position;
		}
		
		public void SetLevel(int level)
		{
			_level = level;
		}
		
		public string GetObjectType()
		{
			return _objType;
		}
		
		public Vector3 GetPosition()
		{
			return _position;
		}
		
		public int GetLevel()
		{
			return _level;
		}
		
		public int GetCreepCost()
		{
			switch(_objType)
			{
			case "Bullfrog":
				_creepCost = 1;
				break;
			case "Boomer":
				_creepCost = 2;
				break;
			case "Cannoneer":
				_creepCost = 2;
				break;
			case "Jarhead":
				_creepCost = 2;
				break;
			case "Raptor":
				_creepCost = 3;
				break;
			case "Krueger":
				_creepCost = 4;
				break;
			case "Lippschultz":
				_creepCost = 4;
				break;
			case "VonSchnauser":
				_creepCost = 4;
				break;
			case "Gorilla":
				_creepCost = 5;
				break;
			case "Scorpion":
				_creepCost = 6;
				break;
			default:
				_creepCost = 1;
				break;
			}
			
			return _creepCost;
		}
		
		public int GetLevelRequirement()
		{
			switch(_objType)
			{
			case "Bullfrog":
				_levelRequirement = 1;
				break;
			case "Boomer":
				_levelRequirement = 1;
				break;
			case "Cannoneer":
				_levelRequirement = 1;
				break;
			case "Jarhead":
				_levelRequirement = 1;
				break;
			case "Raptor":
				_levelRequirement = 2;
				break;
			case "Krueger":
				_levelRequirement = 2;
				break;
			case "Lippschultz":
				_levelRequirement = 3;
				break;
			case "VonSchnauser":
				_levelRequirement = 4;
				break;
			case "Gorilla":
				_levelRequirement = 3;
				break;
			case "Scorpion":
				_levelRequirement = 4;
				break;
			default:
				_levelRequirement = 1;
				break;
			}
			
			return _levelRequirement;
		}
	}
}