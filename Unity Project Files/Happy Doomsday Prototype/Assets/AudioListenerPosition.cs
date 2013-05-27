using UnityEngine;
using System.Collections;

public class AudioListenerPosition : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		CameraScript cscript = Camera.mainCamera.GetComponent<CameraScript>();
		Vector3 camPos = Camera.mainCamera.transform.position;
		camPos += Camera.mainCamera.transform.forward * cscript.distance;
		
		transform.position = camPos;
	}
}
