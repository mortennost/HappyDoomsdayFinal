using UnityEngine;
using System.Collections;

public class LoadingBoxScript : MonoBehaviour
{	
	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
		transform.Rotate(new Vector3(0.0f, 25.0f * Time.deltaTime, 0.0f), Space.World);
	}
}
