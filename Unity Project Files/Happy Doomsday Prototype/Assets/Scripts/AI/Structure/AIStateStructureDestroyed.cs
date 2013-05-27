using UnityEngine;
using System.Collections;

public class AIStateStructureDestroyed : State {
	
	//private static Mesh mesh;
	
	float repairTimer;
	int nextSmokeTime;
	private static GameObject _particleManager;
	
	public AIStateStructureDestroyed( GameObject gameObject ) : base( gameObject ) {
		
	}	
	
	public override void OnStart()
	{
		
		_particleManager = GameObject.Find( "ParticleManager" );
		
		//mesh = GetGameObject().GetComponent<MaterialScript>().debrisMesh;
		
		//GameObject temp = GameObject.Instantiate( Resources.Load( "Prefabs/Structures/Debrie" ) );
		//meshObject = GameObject.Instantiate( "Debris" );
		GetGameObject().tag = "Untargetable";
		
		//GetGameObject().GetComponentInChildren<MeshFilter>().mesh = mesh;
		if(GetGameObject().transform.FindChild("Model") != null)
		{				
			GetGameObject().transform.FindChild("Model").gameObject.SetActive(false);
			GetGameObject().transform.FindChild("Model2").gameObject.SetActive(false);
			GetGameObject().transform.FindChild("Model3").gameObject.SetActive(false);
			GetGameObject().transform.FindChild("DebrisModel").gameObject.SetActive(true);
		}
		
		if(GetGameObject().transform.FindChild("Texture") != null)
		{				
			GetGameObject().transform.FindChild("Texture").gameObject.SetActive(false);
			GetGameObject().transform.FindChild("DebrisModel").gameObject.SetActive(true);
		}
		

		if(GetGameObject().transform.FindChild("ParticleEffect") != null)
		{
			GetGameObject().transform.FindChild("ParticleEffect").gameObject.SetActive(false);
		}
		
		_particleManager.GetComponent<ParticleManager>().AddParticle(
			"BuildingDestruction",
			GetGameObject().transform.position + Vector3.up,
			Quaternion.identity );
		/*
		StructureScript ss = GetGameObject().GetComponent<StructureScript>();
		
		Vector3 corner = GetGameObject().transform.position;
		corner.x -= ((float)ss.xSize/2.0f);
		corner.z -= ((float)ss.zSize/2.0f);
		
		GameObject.Find("Grid").GetComponent<GridScript>().DirGraph.ToggleTraversable(corner, ss.xSize, ss.zSize, true);
		*/
		repairTimer = 0.0f;
		nextSmokeTime = 0;
		
	}
	public override void OnPause() {}
	public override void OnContinue() {}
	public override void OnStop() {}
	
	public override void OnExecute() 
	{
		if (GameObject.FindGameObjectWithTag("Enemy") == null)
		{
			if ( repairTimer < GetGameObject().GetComponent<StructureScript>().BuildTime / 2 ) 
			{
				repairTimer += Time.deltaTime;
				if ( repairTimer >= nextSmokeTime )
				{
					_particleManager.GetComponent<ParticleManager>().AddParticle(
						"BuildingDebrisSmoke",
						GetGameObject().transform.position + Vector3.up,
						Quaternion.identity );
					
					nextSmokeTime += 4;
				}
				//Debug.Log("Repairing: " + repairTimer);
			}
			else 
			{
				if(GetGameObject().GetComponent<ModelScript>() != null)
				{
					GetGameObject().GetComponent<ModelScript>().GetCorrectModel();
				}
				
				GetGameObject().transform.FindChild("DebrisModel").gameObject.SetActive(false);
	
				if(GetGameObject().transform.FindChild("Texture") != null)
				{				
					GetGameObject().transform.FindChild("Texture").gameObject.SetActive(true);
				}
				
				if(GetGameObject().transform.FindChild("ParticleEffect") != null)
				{
					GetGameObject().transform.FindChild("ParticleEffect").gameObject.SetActive(true);
				}
				
				GetGameObject().GetComponent<Health>().UpdateStats();
				
				GetGameObject().GetComponent<StructureStateManager>().ChangeState ( new AIStateStructureOperational( GetGameObject() ) );
				//Debug.Log("Finished repairing structure");
			}
		}
	}	
}
