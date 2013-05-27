using UnityEngine;
using System.Collections.Generic;
using AssemblyCSharp;

public class GridScript : MonoBehaviour
{
	public int gridSize;
	public int unitSize;
	
	private static DirectedGraph _dirGraph;
	
	public DirectedGraph DirGraph {
		get { return _dirGraph; }
	}
	
	// Use this for initialization
	void Start()
	{
		_dirGraph = new DirectedGraph( gridSize, unitSize );
		// Lock grid to origin
		transform.position = Vector3.zero;
		
		StructureScript structureScript = GameObject.Find("Playerhouse").GetComponent<StructureScript>();
		Vector3 cornerPosition = GameObject.Find("Playerhouse").transform.position;
		cornerPosition.x -= structureScript.xSize / 2.0f;
		cornerPosition.z -= structureScript.zSize / 2.0f;
		GameObject.Find("Grid").GetComponent<GridScript>().DirGraph.ToggleTraversable(cornerPosition, structureScript.xSize, structureScript.zSize, true);
	}
	
	// Update is called once per frame
	void Update()
	{
		// Lock grid to origin
		transform.position = Vector3.zero;
		
		
		DrawGizmos();
		DrawNodes ();
	}
	
	private void DrawNodes() {
		foreach ( KeyValuePair<int, Vertex> pooh in DirGraph.vertices ) {
			
			
			Debug.DrawLine( pooh.Value.Position, pooh.Value.Position + new Vector3( 0, 0.1f, 0 ), pooh.Value.IsTraversable ? Color.green : Color.red );
			
		}
	}
	public Vector3 GetCenter()
	{
		return new Vector3((float)gridSize * (float)unitSize / 2.0f, 0.0f, (float)gridSize * (float)unitSize / 2.0f);
	}
	
	// Draws the grid to help with implementation
	void DrawGizmos()
	{
		for (int x = 0; x <= gridSize; ++x)
		{
			Vector3 start = Vector3.right * x * unitSize;
			Vector3 end = start + Vector3.forward * gridSize * unitSize;
			
			Debug.DrawLine(start, end, Color.gray);
		}
		
		for (int z = 0; z <= gridSize; ++z)
		{
			Vector3 start = Vector3.forward * z * unitSize;
			Vector3 end = start + Vector3.right * gridSize * unitSize;
			
			Debug.DrawLine(start, end, Color.gray);
		}
	}
}
