using UnityEngine;
using System.Collections;

public class MoveToTarget : MonoBehaviour {

	bool movingHome = false;
	Hashtable ht = new Hashtable();
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!movingHome)
		{
			ht.Clear();
			ht.Add("z", 30.0f);
			ht.Add("easetype", iTween.EaseType.linear);
			ht.Add("speed", 5.0f);
			ht.Add("looktarget", new Vector3(transform.position.x, transform.position.y, 30.0f));
			ht.Add("islocal", true);
			iTween.MoveTo(gameObject, ht);
			
			if(transform.position.z >= 30.0f)
			{
				movingHome = true;
			}
		}
		else
		{
			ht.Clear();
			ht.Add("z", 3.0f);
			ht.Add("easetype", iTween.EaseType.linear);
			ht.Add("speed", 5.0f);
			ht.Add("looktarget", new Vector3(transform.position.x, transform.position.y, 3.0f));
			ht.Add("islocal", true);
			iTween.MoveTo(gameObject, ht);
			
			if(transform.position.z <= 3.0f)
			{
				movingHome = false;
			}
		}
	}
}
