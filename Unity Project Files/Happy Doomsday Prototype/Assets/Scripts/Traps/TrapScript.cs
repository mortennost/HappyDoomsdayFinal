using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class TrapScript : MonoBehaviour
{
	public bool isOperational;
	public bool isPlaced;
	public float offset;
	// Use this for initialization
	void Start()
	{
		isOperational = true;
		isPlaced = false;
		
		gameObject.transform.FindChild("Texture").gameObject.renderer.materials[0].SetTextureOffset("_MainTex", new Vector2(offset, 0.0f));
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if ( isPlaced && isOperational )
		{
			
			GameObject tempTarget = GetComponent<Target>().FindNearestTarget();
			if ( tempTarget != null )
			{
				GetComponent<Target>().SetTarget( tempTarget );
				
				if ( GetComponent<Attack>().InAttackRange() )
				{
					GetComponent<Attack>().DealDamage();
					isOperational = false;
				}
			}
		}

		if ( isPlaced && !isOperational )
		{
			GameObject.Destroy( gameObject );
		}
	}
}