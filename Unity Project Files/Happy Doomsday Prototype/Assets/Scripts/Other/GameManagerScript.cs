using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	
	public bool isInvasion;
	public bool IsInvasion
	{
		get { return isInvasion; }
		set { isInvasion = value; }
	}
	
	public float timeBetweenInvasions;
	public float timeUntilNextInvasion;
	
	void Awake()
	{
		timeUntilNextInvasion = timeBetweenInvasions * 60.0f;
		isInvasion = false;
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isInvasion)
		{
			timeUntilNextInvasion -= Time.deltaTime;
			
			if (timeUntilNextInvasion <= 0.0f)
			{
				timeUntilNextInvasion = timeBetweenInvasions * 60.0f;
				IsInvasion = true;
				GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new StartupInvasionState(GameObject.Find("GameStateManager")));
			}
		}
		else
		{
			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				isInvasion = false;
				GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new PlayState(GameObject.Find("GameStateManager")));
			}
		}
	}
	
	public void ResetInvasionTimer()
	{
		timeUntilNextInvasion = timeBetweenInvasions * 60.0f;
	}
}
