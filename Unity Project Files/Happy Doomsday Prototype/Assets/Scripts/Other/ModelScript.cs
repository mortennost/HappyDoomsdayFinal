using UnityEngine;
using System.Collections;

public class ModelScript : MonoBehaviour 
{
	//Vector2 offset;
	
	// Use this for initialization
	void Start () 
	{
		//offset = gameObject.transform.FindChild("Texture").gameObject.renderer.materials[0].mainTextureOffset;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		GetCorrectModel();
	}
	
	public void GetCorrectModel()
	{
		if(!gameObject.GetComponent<StructureScript>().isBuilding)
		{
			if(gameObject.GetComponent<Health>().getHealth() > 0)
			{
				if(gameObject.GetComponent<Level>().GetLevel() < 2)
				{
					gameObject.transform.FindChild("Model").gameObject.SetActive(true);
					gameObject.transform.FindChild("Model2").gameObject.SetActive(false);
					gameObject.transform.FindChild("Model3").gameObject.SetActive(false);		
				}
				else if(gameObject.GetComponent<Level>().GetLevel() >= 2 && gameObject.GetComponent<Level>().GetLevel() < 3)
				{
					gameObject.transform.FindChild("Model").gameObject.SetActive(false);
					gameObject.transform.FindChild("Model2").gameObject.SetActive(true);
					gameObject.transform.FindChild("Model3").gameObject.SetActive(false);	
				}
				else if(gameObject.GetComponent<Level>().GetLevel() >= 3)
				{
					gameObject.transform.FindChild("Model").gameObject.SetActive(false);
					gameObject.transform.FindChild("Model2").gameObject.SetActive(false);
					gameObject.transform.FindChild("Model3").gameObject.SetActive(true);
				}
			}
		}
	}
}
