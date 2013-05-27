using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {
	
	public Transform _acidImpact;
	public Transform _explosionImpact;
	
	public Transform _flameAttack;
	public Transform _iceAttack;
	public Transform _psychicAttack;
	
	public Transform _lightningAttack;
	public Transform _lazerAttack;
	
	public Transform _achievement;
	public Transform _levelUp;
	
	public Transform _buildingDebrisSmoke;
	public Transform _buildingDestruction;
	
	public Transform _foodPickup;
	public Transform _waterPickup;
	public Transform _scrapPickup;
	
	public Transform _lightningTrap;
	public Transform _mindControlTrap;
	public Transform _mineTrap;
	
	
	
	private Transform _curParticle;
	
	public void AddParticle( string particleName, Vector3 position, Quaternion rotation )
	{
		//print("checking for particle name match" );
		switch(particleName)
		{
		case "AcidImpact":
			_curParticle = _acidImpact;
			break;
		case "ExplosionImpact":
			_curParticle = _explosionImpact;
			break;
		case "FlameAttack":
			_curParticle = _flameAttack;
			break;
		case "IceAttack":
			_curParticle = _iceAttack;
			break;
		case "PsychicAttack":
			_curParticle = _psychicAttack;
			break;
		case "LightningAttack":
			_curParticle = _lightningAttack;
			break;
		case "LazerAttack":
			_curParticle = _lazerAttack;
			break;
		case "Achievement":
			_curParticle = _achievement;
			break;
		case "LevelUp":
			_curParticle = _levelUp;
			break;
		case "BuildingDebrisSmoke":
			_curParticle = _buildingDebrisSmoke;
			break;
		case "BuildingDestruction":
			_curParticle = _buildingDestruction;
			break;
		case "FoodPickup":
			_curParticle = _foodPickup;
			break;
		case "WaterPickup":
			_curParticle = _waterPickup;
			break;
		case "ScrapPickup":
			_curParticle = _scrapPickup;
			break;
		case "LightningTrap":
			_curParticle = _lightningTrap;
			break;
		case "MindControllTrap":
			_curParticle = _mindControlTrap;
			break;
		case "MineTrap":
			_curParticle = _mineTrap;
			break;
		default:
			_curParticle = null;
			break;	
		}
		
		if ( _curParticle == null )
		{
			print ( "Particle has not been assigned or does not exist" );
		}
		else
		{
			Instantiate ( _curParticle, position, rotation );
		}
	}
}
