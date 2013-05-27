using UnityEngine;
using System.Collections;

public class HealthbarUpdater : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Health healthComp = transform.parent.transform.parent.GetComponent<Health>();
		
		if (healthComp != null)
		{
			Debug.Log ("UPDATING HELATH!");
			Vector3 scale = transform.localScale;
			scale.x = (float)healthComp.getHealth() / (float)healthComp.MaxHealth;
			transform.localScale = scale;
		}
	}
}
