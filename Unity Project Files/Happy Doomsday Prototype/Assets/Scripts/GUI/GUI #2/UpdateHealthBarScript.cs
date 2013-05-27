using UnityEngine;
using System.Collections;

public class UpdateHealthBarScript : MonoBehaviour 
{	
	Health health;
	int lastHealth;
	float totalVisibilityTime = 5.0f;
	float visibilityElapsed;
	bool startCounting;
	
	// Use this for initialization
	void Start () 
	{
		if(transform.parent.GetComponent<Health>() != null)
		{
			health = transform.parent.GetComponent<Health>();
			
			lastHealth = health.CurHealth;
			visibilityElapsed = 0.0f;
			startCounting = false;
			transform.FindChild("MaxHealth").gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.parent.GetComponent<Health>() != null)
		{			
			if(lastHealth != health.CurHealth)
			{
				startCounting = true;
			}
			
			if(startCounting)
			{
				visibilityElapsed += Time.deltaTime;
			
				if(visibilityElapsed >= totalVisibilityTime)
				{
					gameObject.transform.FindChild("MaxHealth").gameObject.SetActive(false);
					startCounting = false;
					visibilityElapsed = 0.0f;
				}
				else
				{
					gameObject.transform.FindChild("MaxHealth").gameObject.SetActive(true);
				}
				
				lastHealth = health.CurHealth;
				
				Vector3 scale = transform.FindChild("MaxHealth").transform.FindChild("Root").localScale;
				scale.x = health.CurHealth / (float)health.MaxHealth;
				transform.FindChild("MaxHealth").transform.FindChild("Root").localScale = scale;
			}
		}
	}
}
