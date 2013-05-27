using UnityEngine;
using System.Collections;
using iGUI;

public class iGUICode_Tutorial : MonoBehaviour
{
	
	[HideInInspector]
	public iGUIImage tutorialImage;
	
	public Texture page1;
	public Texture page2;
	public Texture page3;
	public Texture page4;
	public Texture page5;
	public Texture page6;
	public Texture page7;
	public Texture page8;
	public Texture page9;
	public Texture page10;
	public Texture page11;
	public Texture page12;
	public Texture page13;
	public Texture loading;

	static iGUICode_Tutorial instance;
	void Awake()
	{
		instance=this;
	}
	
	public static iGUICode_Tutorial getInstance()
	{
		return instance;
	}

	public void tutorialImage_Click(iGUIImage caller)
	{
		if (tutorialImage.image.name.Equals("Slide1"))
		{
			tutorialImage.image = page2;
		}
		else if (tutorialImage.image.name.Equals("Slide2"))
		{
			tutorialImage.image = page3;
		}
		else if (tutorialImage.image.name.Equals("Slide3"))
		{
			tutorialImage.image = page4;
		}
		else if (tutorialImage.image.name.Equals("Slide4"))
		{
			tutorialImage.image = page5;
		}
		else if (tutorialImage.image.name.Equals("Slide5"))
		{
			tutorialImage.image = page6;
		}
		else if (tutorialImage.image.name.Equals("Slide6"))
		{
			tutorialImage.image = page7;
		}
		else if (tutorialImage.image.name.Equals("Slide7"))
		{
			tutorialImage.image = page8;
		}
		else if (tutorialImage.image.name.Equals("Slide8"))
		{
			tutorialImage.image = page9;
		}
		else if (tutorialImage.image.name.Equals("Slide9"))
		{
			tutorialImage.image = page10;
		}
		else if (tutorialImage.image.name.Equals("Slide10"))
		{
			tutorialImage.image = page11;
		}
		else if (tutorialImage.image.name.Equals("Slide11"))
		{
			tutorialImage.image = page12;
		}
		else if (tutorialImage.image.name.Equals("Slide12"))
		{
			tutorialImage.image = page13;
		}
		else if (tutorialImage.image.name.Equals("Slide13"))
		{
			tutorialImage.image = loading;
			Application.LoadLevel("CompoundNewGUI");
		}
	}
}
