using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Vertex
	{
		private readonly int index;
		public int Index
		{
			get { return index; }
		}
		
		private readonly Vector3 position;
		public Vector3 Position
		{
			get { return position; }
		}
		
		private bool isTraversable;
		public bool IsTraversable
		{
			set { isTraversable = value; }
			get { return isTraversable; }
		}
		
		public Vertex(int index, Vector3 position)
		{
			IsTraversable = true;
			this.index = index;
			this.position = position;
		}
		
		public bool ToggleTraversable()
		{
			return IsTraversable = !IsTraversable;
		}
	}
}

