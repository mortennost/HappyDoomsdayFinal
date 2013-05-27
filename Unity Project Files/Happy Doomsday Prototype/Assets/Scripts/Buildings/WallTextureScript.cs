using UnityEngine;
using System.Collections;

public class WallTextureScript : MonoBehaviour 
{
	
	Texture currentTexture;
	
	public Texture texture1;
	public Texture texture2;
	public Texture texture3;
	
	bool textureIsSet;
	
	// Use this for initialization
	void Start () 
	{
		textureIsSet = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!textureIsSet)
		{
			GetCorrectTexture();
			textureIsSet = true;
		}
	}
	
	public void GetCorrectTexture()
	{
		int num = Random.Range(1, 4);
		print (num);
		
		if(num == 1)
		{
			currentTexture = texture1;
		}
		else if(num == 2)
		{
			currentTexture = texture2;
		}
		else
		{
			currentTexture = texture3;
		}
		
		gameObject.transform.FindChild("Texture").gameObject.renderer.materials[0].mainTexture = currentTexture;
		
		//currentTexture
	}
}
