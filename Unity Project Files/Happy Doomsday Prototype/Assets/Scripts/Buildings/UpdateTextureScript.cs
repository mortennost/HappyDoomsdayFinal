using UnityEngine;
using System.Collections;

public class UpdateTextureScript : MonoBehaviour 
{
	
	Texture currentTexture;
	
	public Texture texture1;
	public Texture texture2;
	public Texture texture3;
	public Texture texture4;
	public Texture texture5;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetCorrectTexture();
	}
	
	public void GetCorrectTexture()
	{
		if(gameObject.GetComponent<Level>().GetLevel() < 2)
		{				
			currentTexture = texture1;
		}
		else if(gameObject.GetComponent<Level>().GetLevel() >= 2 && gameObject.GetComponent<Level>().GetLevel() < 3)
		{
			currentTexture = texture2;
		}
		else if(gameObject.GetComponent<Level>().GetLevel() >= 3 && gameObject.GetComponent<Level>().GetLevel() < 4)
		{
			currentTexture = texture3;
		}
		else if(gameObject.GetComponent<Level>().GetLevel() >= 4 && gameObject.GetComponent<Level>().GetLevel() < 5)
		{
			currentTexture = texture4;
		}
		else if(gameObject.GetComponent<Level>().GetLevel() >= 5)
		{
			currentTexture = texture5;
		}
		
		gameObject.transform.FindChild("Texture").gameObject.renderer.materials[0].mainTexture = currentTexture;
		
		//currentTexture
	}
}
