using UnityEngine;
using System.Collections;

public class LookAtCameraOnly : MonoBehaviour 
{
	Camera cameraToLookAt;
	
	void Start() {
		
	}
 
    void Update() 
    {
		cameraToLookAt = Camera.mainCamera;
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position - v); 
		
		renderer.material.renderQueue = 1;
    }
}
