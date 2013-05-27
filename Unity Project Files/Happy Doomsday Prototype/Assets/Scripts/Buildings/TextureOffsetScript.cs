using UnityEngine;
using System.Collections;

public class TextureOffsetScript : MonoBehaviour 
{
	Vector2 offset;
	
	// Use this for initialization
	void Start () 
	{
		offset = gameObject.transform.FindChild("Texture").gameObject.renderer.materials[0].mainTextureOffset;
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetCorrectTexture();
	}
	
	public void GetCorrectTexture()
	{
		if(gameObject.GetComponent<Health>().getHealth() > 0)
		{
			if(gameObject.GetComponent<Level>().GetLevel() < 2)
			{
				offset.x = -0.4f;
				//gameObject.transform.FindChild("Model").gameObject.SetActive(true);
				//gameObject.transform.FindChild("Model2").gameObject.SetActive(false);
				//gameObject.transform.FindChild("Model3").gameObject.SetActive(false);				
			}
			else if(gameObject.GetComponent<Level>().GetLevel() >= 2 && gameObject.GetComponent<Level>().GetLevel() < 3)
			{
				offset.x = -0.2f;
				//gameObject.transform.FindChild("Model").gameObject.SetActive(false);
				//gameObject.transform.FindChild("Model2").gameObject.SetActive(true);
				//gameObject.transform.FindChild("Model3").gameObject.SetActive(false);
			}
			else if(gameObject.GetComponent<Level>().GetLevel() >= 3 && gameObject.GetComponent<Level>().GetLevel() < 4)
			{
				offset.x = 0.0f;
				//gameObject.transform.FindChild("Model").gameObject.SetActive(false);
				//gameObject.transform.FindChild("Model2").gameObject.SetActive(true);
				//gameObject.transform.FindChild("Model3").gameObject.SetActive(false);
			}
			else if(gameObject.GetComponent<Level>().GetLevel() >= 4 && gameObject.GetComponent<Level>().GetLevel() < 5)
			{
				offset.x = 0.2f;
				//gameObject.transform.FindChild("Model").gameObject.SetActive(false);
				//gameObject.transform.FindChild("Model2").gameObject.SetActive(true);
				//gameObject.transform.FindChild("Model3").gameObject.SetActive(false);
			}
			else if(gameObject.GetComponent<Level>().GetLevel() >= 5)
			{
				offset.x = 0.4f;
				//gameObject.transform.FindChild("Model").gameObject.SetActive(false);
				//gameObject.transform.FindChild("Model2").gameObject.SetActive(false);
				//gameObject.transform.FindChild("Model3").gameObject.SetActive(true);
			}
			
			gameObject.transform.FindChild("Texture").gameObject.renderer.materials[0].SetTextureOffset("_MainTex", offset);
		}
	}
}
