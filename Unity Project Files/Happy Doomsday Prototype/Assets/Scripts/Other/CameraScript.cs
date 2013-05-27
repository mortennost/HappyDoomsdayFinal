using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CameraScript : MonoBehaviour {
	
	public GameObject target;
	public float damping;
	public float defaultSize = 3.0f;
	
	Vector3 desiredPosition;
	public float distance = 500.0f;

	// Use this for initialization
	void Start ()
	{
		Camera.mainCamera.orthographicSize = defaultSize;
		transform.position = GameObject.Find("Grid").GetComponent<GridScript>().GetCenter();
		transform.position -= transform.forward * distance;
		desiredPosition = transform.position;
		
		//GameCenterSingleton gcSingleton = GameCenterSingleton.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null)
		{
			desiredPosition = target.transform.position;
			desiredPosition -= transform.forward * distance;
			
			Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
			transform.position = position;
			
			// Enable TargetRadius
			if(target.transform.FindChild("TargetRadius") != null)
			{
				target.transform.FindChild("TargetRadius").gameObject.SetActive(true);
			}
		}
		else
		{
			Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
			transform.position = position;
		}
	}
	
	public void SetDesiredPosition(Vector3 position)
	{
		desiredPosition = position;
	}
	
	public Vector3 GetDesiredPosition()
	{
		return desiredPosition;
	}
}
