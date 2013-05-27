using UnityEngine;
using System.Collections;

public class BillboardScript : MonoBehaviour {
	
	float maxHealth;
	float health;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		maxHealth = transform.parent.parent.GetComponent<Health>().MaxHealth;
		health = transform.parent.parent.GetComponent<Health>().CurHealth;
		
		if(health > maxHealth)
		{
			health = maxHealth;
		}
		
		Vector3 newScale = new Vector3(health / maxHealth, transform.localScale.y, transform.localScale.z);
		transform.localScale = newScale;
		
		if(health <= 0 || health >= maxHealth)
		{
			renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
			transform.parent.localScale = new Vector3(0.0f, transform.parent.localScale.y, transform.parent.localScale.z);
		}
		else
		{
			renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			transform.parent.localScale = new Vector3(1.0f, transform.parent.localScale.y, transform.parent.localScale.z);
			transform.parent.renderer.materials = GameObject.Find("Billboard Background").GetComponent<BillboardBackgroundScript>().materials;
		}
	}
}
