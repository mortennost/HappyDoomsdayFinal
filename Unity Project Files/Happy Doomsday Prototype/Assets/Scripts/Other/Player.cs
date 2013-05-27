using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	private uint _experience;
	private uint _tempExperience;
	private uint _food;
	private uint _money;
	private uint _realMoney;
	
	
	// Use this for initialization
	void Start () {
	// probably some DB handling and init variables.
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/* Checks if theres enough food to do an assignment
	 * and returns true or false and removes food.
	 */
	public bool UseFood( uint amount ) {
		
		if ( _food >= amount ) {
			RemoveFood ( amount );
			return true;
		} else {
			return false;
		}
	}
	
	/* adds Temporary Experience */
	public void AddTempExperience( uint experienceToAdd ) { _tempExperience += experienceToAdd;	}
	/* updates accumulated experience */
	public void UpdateExperience() {
		_experience += _tempExperience;
		_tempExperience = 0;
	}
	
	/* adds food */
	public void AddFood( uint foodToAdd ) { _food += foodToAdd; }
	/* removes food */
	public void RemoveFood( uint foodToRemove ) { _food -= foodToRemove; }
	/* returns food */
	public uint GetFood() { return _food; }
	/* adds Money */
	
	public void AddMoney( uint moneyToAdd ) { _money += moneyToAdd; }
	/* removes money */
	public void RemoveMoney( uint moneyToRemove ) { _money -= moneyToRemove; }
	/* returns money */
	public uint GetMoney() { return _money; }
	
}