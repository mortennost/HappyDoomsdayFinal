using UnityEngine;
using System.Collections;
using iGUI;

public class TrapInputAction : MonoBehaviour
{
	//Rect panelRect;
	//Rect electricRect;
	//Rect mineRect;
	//Rect controlRect;
	
	//Vector2 swipeStartPosition;
	
	public iGUIButton electric;
	public iGUIButton mine;
	public iGUIButton control;
	
	[HideInInspector]
	public bool instantiated;
	
	// Use this for initialization
	void Start()
	{
		//panelRect = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._panelTraps.rect;
		//electricRect = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUseElectricTrap.rect;
		//mineRect = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUseMineTrap.rect;
		//controlRect = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUseControlTrap.rect;
		
		//swipeStartPosition = Vector2.zero;
		
		instantiated = false;
		
		//electric = GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnUseElectricTrap;
	}
	
	// Update is called once per frame
	void Update() 
	{
		/*
		if(!instantiated)
		{
			if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				Ray currentRay = Camera.mainCamera.ScreenPointToRay(new Vector3(Screen.width /2.0f, Screen.height /2.0f, 0.0f));
				Plane plane = new Plane(Vector3.up, Vector3.zero);
				float currentEnter = 0.0f;
				plane.Raycast(currentRay, out currentEnter);
				Vector3 newGridsnapperPos = currentRay.origin + currentEnter * currentRay.direction;
				GameObject.Find("Gridsnapper").transform.position = newGridsnapperPos;
				
				swipeStartPosition = Input.GetTouch(0).position;
				
				swipeStartPosition.y = Screen.height - swipeStartPosition.y;
				
				GameObject.Find("Input Manager").GetComponent<BuildTrapInputLayer>().snapperSwipeBeganPosition = GameObject.Find("Gridsnapper").transform.position;
			}
			
			if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				if (swipeStartPosition.x > panelRect.x && swipeStartPosition.x < panelRect.x + panelRect.width &&
					swipeStartPosition.y > panelRect.y && swipeStartPosition.y < panelRect.y + panelRect.height)
				{
					if (
						(swipeStartPosition.x > electricRect.x && swipeStartPosition.x < electricRect.x + electricRect.width &&
						swipeStartPosition.y > electricRect.y && swipeStartPosition.y < electricRect.y + electricRect.height) &&
						
						(Screen.height - Input.GetTouch(0).position.y) < panelRect.y)
					{
						Ray currentRay = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
						Plane plane = new Plane(Vector3.up, Vector3.zero);
						float currentEnter = 0.0f;
						plane.Raycast(currentRay, out currentEnter);
						GameObject.Find("Input Manager").GetComponent<BuildTrapInputLayer>().swipeBeganPosition = currentRay.origin + currentEnter * currentRay.direction;
						

						electric.GetComponent<InstantiateTrapAction>().PlaceTrap();
						instantiated = true;
					}
				}
			}
			
			if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				swipeStartPosition = Vector2.zero;
			}
		}
		*/
	}
}
