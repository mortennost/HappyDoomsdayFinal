using UnityEngine;
using System.Collections;

public class InvasionInputLayer : InputLayer
{
	//InputHandler inputHandler;
	CameraScript cameraScript;
	float touchPhaseTimer;
	
	Vector3 north;
	Vector3 south;
	Vector3 east;
	Vector3 west;

	// Use this for initialization
	void Start ()
	{
		//inputHandler = gameObject.GetComponent<InputHandler>();
		cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
		touchPhaseTimer = 0.0f;
		Camera.mainCamera.GetComponent<CameraScript>().target = null;
		
		north = Vector3.forward + Vector3.right;
		north.Normalize();
		south = -north;
		
		west = Vector3.forward + Vector3.left;
		west.Normalize();
		east = -west;
	}
	
	// Update is called once per frame
	void Update()
	{
		GameObject.Find("ConstructionHighlight Z").renderer.enabled = false;
		GameObject.Find("ConstructionHighlight X").renderer.enabled = false;
		
		// Pinch Zoom
		if (Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved))
		{
			Vector2 currentDistance = Input.GetTouch(0).position - Input.GetTouch(1).position;
			Vector2 previousDistance = (Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
			float touchDelta = currentDistance.magnitude - previousDistance.magnitude;
			
			Camera.mainCamera.orthographicSize -= touchDelta * 0.01f;
			if (Camera.mainCamera.orthographicSize < 1.0f) Camera.mainCamera.orthographicSize = 1.0f;
			if (Camera.mainCamera.orthographicSize > 7.0f) Camera.mainCamera.orthographicSize = 7.0f;
		}
		
		// Swipe Pan
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && cameraScript.target == null)
		{
			if (cameraScript.target == null)
			{
				Debug.Log("Camera target is NULL, swiping is POSSIBLE!");
			}
			
			Ray currentRay = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
			Ray previousRay = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
			
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			
			float currentEnter = 0.0f;
			float previousEnter = 0.0f;
			
			plane.Raycast(currentRay, out currentEnter);
			plane.Raycast(previousRay, out previousEnter);
			
			Vector3 currentPosition = currentRay.origin + currentEnter * currentRay.direction;
			Vector3 previousPosition = previousRay.origin + previousEnter * previousRay.direction;
			Vector3 deltaPosition = currentPosition - previousPosition;
			
			Vector3 newPosition = Camera.mainCamera.transform.position - deltaPosition;
			newPosition = CheckBounds(newPosition);
			
			cameraScript.SetDesiredPosition(newPosition);
			Camera.mainCamera.transform.position = newPosition;
		}
		
		// Tap on structure (iOS)
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{			
			// Tap
			if (touchPhaseTimer < 0.5f)
			{
				Ray currentRay = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
				Plane plane = new Plane(Vector3.up, Vector3.zero);
				float currentEnter = 0.0f;
				plane.Raycast(currentRay, out currentEnter);
				
				Vector3 currentPosition = currentRay.origin + currentEnter * currentRay.direction;
				
				GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
				
				bool found = false;
				
				foreach (GameObject obj in structures)
				{
					// Check if raycast is within structure's bounds
					if(obj.GetComponent<StructureScript>().IsInsideBounds(currentPosition))
					{
						cameraScript.target = obj;
						found = true;
						obj.GetComponent<StructureScript>().OpenSubMenu(obj);
						break;
					}
				}
				
				if (!found) 
				{
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.enabled = false;
					GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.enabled = false;
					
					cameraScript.target = null;
				}
			}
			
			touchPhaseTimer = 0.0f;
		}
		
		
#if UNITY_WEBPLAYER
		
		if (Input.GetKeyDown(KeyCode.I))
		{
			GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new InvasionState(GameObject.Find("GameStateManager")));
		}
		
		// Click on structure, open SubMenu (PC)
		if (Input.GetKeyDown(KeyCode.Mouse0)) // Click on structure (PC)
		{			
			Ray currentRay = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			float currentEnter = 0.0f;
			plane.Raycast(currentRay, out currentEnter);
			
			Vector3 currentPosition = currentRay.origin + currentEnter * currentRay.direction;
			
			GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
			
			bool found = false;
				
			foreach (GameObject obj in structures)
			{
				// Check if raycast is within structure's bounds
				if(obj.GetComponent<StructureScript>().IsInsideBounds(currentPosition))
				{
					cameraScript.target = obj;
					found = true;
					obj.GetComponent<StructureScript>().OpenSubMenu(obj);
					break;
				}
			}
				
			if (!found) 
			{
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.fadeTo(0.0f, 0.2f);
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.fadeTo(0.0f, 0.2f);
				
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnDestroyStructure.enabled = false;
				GameObject.Find("GUI").GetComponent<iGUICode_GUI_mockup2>()._btnHarvest.enabled = false;
				cameraScript.target = null;
			}
		}
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			Vector3 newPosition = cameraScript.GetDesiredPosition() + north * 20.0f * Time.deltaTime;
			newPosition = CheckBounds(newPosition);
			Debug.Log(newPosition);
			cameraScript.SetDesiredPosition(newPosition);
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			Vector3 newPosition = cameraScript.GetDesiredPosition() + south * 20.0f * Time.deltaTime;
			newPosition = CheckBounds(newPosition);
			cameraScript.SetDesiredPosition(newPosition);
		}
		
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			Vector3 newPosition = cameraScript.GetDesiredPosition() + west * 20.0f * Time.deltaTime;
			newPosition = CheckBounds(newPosition);
			cameraScript.SetDesiredPosition(newPosition);
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			Vector3 newPosition = cameraScript.GetDesiredPosition() + east * 20.0f * Time.deltaTime;
			newPosition = CheckBounds(newPosition);
			cameraScript.SetDesiredPosition(newPosition);
		}
		
		if (Input.GetKeyDown(KeyCode.I))
		{
			GameObject.Find("GameStateManager").GetComponent<GameStateManager>().ChangeState(new PlayState(GameObject.Find("GameStateManager")));
		}
#endif
	}
	
	Vector3 CheckBounds(Vector3 newPosition)
	{
		Vector3 check = newPosition + Camera.mainCamera.transform.forward * cameraScript.distance;
		
		if (check.x < 0.0f) newPosition.x += Mathf.Abs(check.x);
		if (check.x > 32.0f) newPosition.x -= Mathf.Abs(check.x - 32.0f);
		if (check.z < 0.0f) newPosition.z += Mathf.Abs(check.z);
		if (check.z > 32.0f) newPosition.z -= Mathf.Abs(check.z - 32.0f);
		
		return newPosition;
	}
}
