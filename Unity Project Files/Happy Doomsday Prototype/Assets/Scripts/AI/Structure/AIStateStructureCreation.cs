using UnityEngine;
using System.Collections;

public class AIStateStructureCreation : State {
	
	public int _buildSpeed = 10;
	//private float _nextBuildFase = 0.0f;
	public float _buildTimer = 0.0f;

	public AIStateStructureCreation( GameObject gameObject ) : base( gameObject ) {
		
	}	
	
	public override void OnStart()
	{

		GetGameObject().tag = "Structure";
		
		if(GetGameObject().transform.FindChild("InProgress") != null)
		{
			GetGameObject().transform.FindChild("InProgress").gameObject.SetActive(true);
			
			if(GetGameObject().transform.FindChild("Texture") != null)
			{
				GetGameObject().transform.FindChild("Texture").gameObject.SetActive(false);
			}
			else if(GetGameObject().transform.FindChild("Model") != null)
			{
				GetGameObject().transform.FindChild("Model").gameObject.SetActive(false);
				GetGameObject().transform.FindChild("Model2").gameObject.SetActive(false);
				GetGameObject().transform.FindChild("Model3").gameObject.SetActive(false);
			}
			
			
			GetGameObject().transform.FindChild("DebrisModel").gameObject.SetActive(false);
		}
		
		if(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWorkerCount() < GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxWorkerCount())
		{
			GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().AddWorker();
		}
	}
	public override void OnPause() {}
	public override void OnContinue() {}
	public override void OnStop()
	{
		GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().RemoveWorker();
	}
	
	public override void OnExecute() {
		
		if(GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetWorkerCount() <= GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>().GetMaxWorkerCount())
		{
			if ( _buildTimer < GetGameObject().GetComponent<StructureScript>().BuildTime ) 
			{
				_buildTimer += Time.deltaTime;
				GetGameObject().GetComponent<StructureScript>().isBuilding = true;
				//Debug.Log("Building: " + _buildTimer + " / " + GetGameObject().GetComponent<StructureScript>().BuildTime);
			}
			else 
			{
				GetGameObject().GetComponent<StructureStateManager>().ChangeState ( new AIStateStructureOperational( GetGameObject() ) );
				//Debug.Log("Finished building structure");
				GetGameObject().GetComponent<StructureScript>().isBuilding = false;
			}
			
			if( GetGameObject().GetComponent<Health>().getHealth() <= 0 )
			{
				GetGameObject().GetComponent<StructureStateManager>().ChangeState ( new AIStateStructureDestroyed( GetGameObject() ) );
			}
		}
	}
	
	public void InstaBuild()
	{
		_buildTimer = GetGameObject().GetComponent<StructureScript>().BuildTime;
	}
}
