using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class CreepSpawnScript : MonoBehaviour 
{
	public GameObject _bullfrog;
	public GameObject _boomer;
	public GameObject _cannoneer;
	public GameObject _jarhead;
	public GameObject _raptor;
	public GameObject _krueger;
	public GameObject _lippschultz;
	public GameObject _vonschnauser;
	public GameObject _gorilla;
	public GameObject _scorpion;
	
	private int _bossCreep;
	private int _spawnLvl;
	private string _movieToPlay;
	
	Stack<SpawnType> _spawnStack;
	
	public void AddSpawnStack(Stack<SpawnType> stack)
	{
		_spawnStack = stack;
	}
	
	public GameObject SpawnCreep(string type, Vector3 position)
	{
		GameObject tempObject = null;
		
		switch(type)
			{
			case "Bullfrog":
				tempObject = Instantiate(_bullfrog) as GameObject;
				_bullfrog.transform.position = position;
				_bullfrog.GetComponent<Level>().SetLevel(_spawnLvl);
				break;
			case "Boomer":
				tempObject = Instantiate(_boomer) as GameObject;
				_boomer.transform.position = position;
				_boomer.GetComponent<Level>().SetLevel(_spawnLvl);
				break;
			case "Cannoneer":
				tempObject = Instantiate(_cannoneer) as GameObject;
				_cannoneer.transform.position = position;
				_cannoneer.GetComponent<Level>().SetLevel(_spawnLvl);
				break;
			case "Jarhead":
				tempObject = Instantiate(_jarhead) as GameObject;
				_jarhead.transform.position = position;
				_jarhead.GetComponent<Level>().SetLevel(_spawnLvl);
				break;
			case "Raptor":
				tempObject = Instantiate(_raptor) as GameObject;
				_raptor.transform.position = position;
				_raptor.GetComponent<Level>().SetLevel(_spawnLvl);
				break;
			case "Krueger":
				tempObject = Instantiate(_krueger) as GameObject;
				_krueger.transform.position = position;
				_krueger.GetComponent<Level>().SetLevel(_spawnLvl);
				break;
			case "Lippschultz":
				tempObject = Instantiate(_lippschultz) as GameObject;
				_lippschultz.transform.position = position;
				_lippschultz.GetComponent<Level>().SetLevel(_spawnLvl);
				break;
			case "VonSchnauser":
				tempObject = Instantiate(_vonschnauser) as GameObject;
				_vonschnauser.transform.position = position;
				_vonschnauser.GetComponent<Level>().SetLevel(_spawnLvl);
				break;
			case "Gorilla":
				tempObject = Instantiate(_gorilla) as GameObject;
				_gorilla.transform.position = position;
				_gorilla.GetComponent<Level>().SetLevel(_spawnLvl);
				break;
			case "Scorpion":
				tempObject = Instantiate(_scorpion) as GameObject;
				_scorpion.transform.position = position;
				_scorpion.GetComponent<Level>().SetLevel(_spawnLvl);
				break;
			default:
				break;
			}
		
		return tempObject;
	}
	
	public void SpawnStack()
	{
		_spawnLvl = GameObject.Find("Playerhouse").GetComponent<Level>().GetLevel();
		
		_bossCreep = (int)( Random.value * _spawnStack.Count );
		
		GameObject tempCreep;
		
		while(_spawnStack.Count > 0)
		{
			tempCreep = SpawnCreep(_spawnStack.Peek().GetObjectType(), _spawnStack.Peek().GetPosition());
			
			if ( _bossCreep == _spawnStack.Count )
			{
				tempCreep.transform.localScale = new Vector3( 2.0f, 2.0f, 2.0f );
				_movieToPlay = _spawnStack.Peek().GetObjectType() + ".mp4";
			}
			_spawnStack.Pop();
		}
		
		/*
		if( System.IO.File.Exists( Application.dataPath + "/StreamingAssets/" +_movieToPlay ) )
	    {
	       	print ("File EXISTS");    
	    } else {
			print ("file does not exist.");
		}*/
		
#if UNITY_IPHONE
		Handheld.PlayFullScreenMovie( Application.dataPath + "/StreamingAssets/" + _movieToPlay, Color.black, FullScreenMovieControlMode.CancelOnInput );
#endif
	}
}
