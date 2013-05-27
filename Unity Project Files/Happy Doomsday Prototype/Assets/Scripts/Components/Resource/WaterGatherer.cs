using UnityEngine;
using System.Collections;

public class WaterGatherer : MonoBehaviour {
	
	
	public float _baseGatheringSpeedPerSecond = 2;
	public float _gatheringSpeedPerSecondModifier = 1.0f;
	
	public float _baseFillUpTime = 10.0f;
	public float _fillUpTimeModifier = 1.0f;
	
	private float _gatheringSpeedPerSecond;
	private int _maxWaterGatherable;
	private float _accumulatedWater;
	
	public float GatheringSpeed {
		set { _gatheringSpeedPerSecond = value; }
		get { return _gatheringSpeedPerSecond; }
	}
	public int MaxWater{
		set { _maxWaterGatherable = value; }
		get { return _maxWaterGatherable; }
	}
	public float AccumulatedWater {
		set
		{
			_accumulatedWater = value;
			if ( _accumulatedWater > MaxWater )
				_accumulatedWater = MaxWater;
			else if ( _accumulatedWater < 0 )
				_accumulatedWater = 0;
		}
		get { return _accumulatedWater; }
	}
	
	// use to gather water.
	public bool Gather()
	{
		AccumulatedWater += GatheringSpeed;
		
		if ( AccumulatedWater == MaxWater )
			return false;
		
		return true;
	}
	
	// use to get accumulated water and empty the harvester.
	public int DepleteWater()
	{
		int r = 0;
		ResourceManagerScript rsm = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>();
		
		if(rsm._waterCount < rsm._maxWaterCount)
		{
			r = (int)AccumulatedWater;
			
			if(rsm._waterCount + r > rsm._maxWaterCount)
			{
				r = rsm._maxWaterCount - rsm._waterCount;
				AccumulatedWater -= r;
			}
			else
			{
				AccumulatedWater = 0;
			}
		}
		
		return r;
	}
	// Use this for initialization
	void Start () {
		AccumulatedWater = 0.0f;
		UpdateStats();
	}
	
	public void UpdateStats()
	{
		int lvl = gameObject.GetComponent<Level>().GetLevel() - 1;
		
		GatheringSpeed = ( _baseGatheringSpeedPerSecond * Mathf.Pow ( _gatheringSpeedPerSecondModifier, lvl ) );
		
		float weirdFillUpTimeThingie = _baseFillUpTime * Mathf.Pow ( _fillUpTimeModifier, lvl );
		
		MaxWater = (int)(GatheringSpeed * weirdFillUpTimeThingie);
		
		//MaxWater = (int) ( _baseMaxWaterGatherable * Mathf.Pow( _maxWaterGatherableModifier, lvl ) );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
