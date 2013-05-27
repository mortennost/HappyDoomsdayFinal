using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int _baseHealth = 100;
	public float _healthModifier = 1.0f;
	
	private int _health;
	private int _maxHealth;
	
	bool isLoaded = false;
	
	
	public int CurHealth {
		set { _health = value; }
		get { return _health; }
	}
	
	public int MaxHealth {
		set { _maxHealth = value; }
		get { return _maxHealth; }
	}

	void Awake()
	{
		
	}
	
	// Use this for initialization
	void Start () {
		
		
		
		
	}
	
	public void UpdateStats()
	{
		int lvl = gameObject.GetComponent<Level>().GetLevel() - 1;
		
		MaxHealth = (int)(_baseHealth * Mathf.Pow( _healthModifier, lvl ));
		
		_health = _maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isLoaded)
		{
			UpdateStats();
			isLoaded = true;
		}
	}
	
	void FixedUpdate() {
	}
	
	public int getHealth() { return _health; }
	public int getMaxHealth() { return _maxHealth; }
	public void addHealth( int health )
	{
		if ( ( _health + health ) > MaxHealth )
		{
			_health = MaxHealth;
		} else {
			_health += health;
		}
		
		//print( gameObject.tag + " health: " + _health );
	}
	public void RemoveHealth( int health ) {
		_health -= health; 
		//print( gameObject.tag + " health: " + _health );
	}
}
