using UnityEngine;
using System.Collections;
using iGUI;

public class GridsnapperScript : MonoBehaviour
{
	public GameObject clone;
	
	public int xSize;
	public int zSize;
	
	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
		Snap();
		DrawGizmos();
		
		if (clone != null)
		{
			clone.transform.position = transform.position;
		}
	}
	
	public void Snap()
	{
		// TODO: Snap to grid
		float gridSize = (float)GameObject.Find("Grid").GetComponent<GridScript>().gridSize;
		float unitSize = (float)GameObject.Find("Grid").GetComponent<GridScript>().unitSize;
		
		Vector3 zLength = new Vector3(0.0f, 0.0f, (float)zSize * unitSize);
		Vector3 xLength = new Vector3((float)xSize * unitSize, 0.0f, 0.0f);
		
		Vector3 minPosition = transform.position - xLength / 2.0f - zLength / 2.0f;
		
		minPosition = new Vector3(minPosition.x / unitSize, 0.0f, minPosition.z / unitSize);
		minPosition = new Vector3(Mathf.Round(minPosition.x), 0.0f, Mathf.Round(minPosition.z));
		
		if (minPosition.x < 0.0f)
		{
			minPosition.x = 0.0f;
		}
		
		if (minPosition.z < 0.0f)
		{
			minPosition.z = 0.0f;
		}
		
		if (minPosition.x > gridSize - (float)xSize)
		{
			minPosition.x = gridSize - (float)xSize;
		}
		
		if (minPosition.z > gridSize - (float)zSize)
		{
			minPosition.z = gridSize - (float)zSize;
		}
		
		minPosition = new Vector3(minPosition.x * unitSize, 0.0f, minPosition.z * unitSize);
		transform.position = minPosition + xLength / 2.0f + zLength / 2.0f;
	}
	
	void DrawGizmos()
	{
		float unitSize = GameObject.Find("Grid").GetComponent<GridScript>().unitSize;
		
		Vector3 zLength = new Vector3(0.0f, 0.0f, (float)zSize * unitSize);
		Vector3 xLength = new Vector3((float)xSize * unitSize, 0.0f, 0.0f);
		
		Vector3 minPosition = transform.position - xLength / 2.0f - zLength / 2.0f;
		minPosition.y += 0.1f;
		Vector3 maxPosition = minPosition + xLength + zLength;
		
		Debug.DrawLine(minPosition, minPosition + zLength, Color.yellow);
		Debug.DrawLine(minPosition, minPosition + xLength, Color.yellow);
		Debug.DrawLine(maxPosition, maxPosition - zLength, Color.yellow);
		Debug.DrawLine(maxPosition, maxPosition - xLength, Color.yellow);
	}
}
