using UnityEngine;
using System.Collections;

public class AIStateStructureOperational : State {

	public AIStateStructureOperational( GameObject gameObject ) : base( gameObject ) {
	}	
	
	public override void OnStart()
	{
		GetGameObject().tag = "Structure";
		
		if(GetGameObject().transform.FindChild("InProgress") != null)
		{
			GetGameObject().transform.FindChild("InProgress").gameObject.SetActive(false);
			GetGameObject().transform.FindChild("DebrisModel").gameObject.SetActive(false);
		}
		
		if(GetGameObject().transform.FindChild("Texture") != null)
		{
			GetGameObject().transform.FindChild("Texture").gameObject.SetActive(true);
		}
		else if(GetGameObject().transform.FindChild("Model") != null)
		{
			GetGameObject().transform.FindChild("Model").gameObject.SetActive(true);
			GetGameObject().transform.FindChild("Model2").gameObject.SetActive(false);
			GetGameObject().transform.FindChild("Model3").gameObject.SetActive(false);
		}
	}
	public override void OnPause() {}
	public override void OnContinue() {}
	public override void OnStop() {}
	
	public override void OnExecute() {
		
		
	}	
}
