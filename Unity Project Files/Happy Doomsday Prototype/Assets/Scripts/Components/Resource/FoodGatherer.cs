using UnityEngine;
using System.Collections;

public class FoodGatherer : MonoBehaviour {
	
	
	public float _baseGatheringSpeedPerSecond = 2.0f;
	public float _gatheringSpeedPerSecondModifier = 1.0f;
	
	public float _baseFillUpTime = 10.0f;
	public float _fillUpTimeModifier = 1.0f;	
	
	private float _gatheringSpeedPerSecond;
	private int _maxFoodGatherable;
	private float _accumulatedFood;
	
	
	public float GatheringSpeed {
		set { _gatheringSpeedPerSecond = value; }
		get { return _gatheringSpeedPerSecond; }
	}
	
	public int MaxFood {
		set { _maxFoodGatherable = value; }
		get { return _maxFoodGatherable; }
	}
	
	public float AccumulatedFood {
		set
		{
			_accumulatedFood = value;
			if ( _accumulatedFood > _maxFoodGatherable )
				_accumulatedFood = _maxFoodGatherable;
			else if ( _accumulatedFood < 0 )
				_accumulatedFood = 0;
		}
		get { return _accumulatedFood; }
	}
	
	public bool Gather()
	{
		AccumulatedFood += GatheringSpeed;
		
		if ( AccumulatedFood == MaxFood )
			return false;
		
		return true;
	}
	
	// use to get accumulated food and empty the harvester.
	public int DepleteFood()
	{
		int r = 0;
		ResourceManagerScript rsm = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>();
		
		if(rsm._foodCount < rsm._maxFoodCount)
		{
			r = (int)AccumulatedFood;
			
			if(rsm._foodCount + r > rsm._maxFoodCount)
			{
				r = rsm._maxFoodCount - rsm._foodCount;
				AccumulatedFood -= r;
			}
			else
			{
				AccumulatedFood = 0;
			}
		}
		
		return r;
	}
	
	// Use this for initialization
	void Start () {
		_accumulatedFood = 0.0f;
		UpdateStats();
	}
	
	public void UpdateStats()
	{
		int lvl = gameObject.GetComponent<Level>().GetLevel() - 1;
		
		GatheringSpeed = ( _baseGatheringSpeedPerSecond * Mathf.Pow ( _gatheringSpeedPerSecondModifier, lvl ) );
		
		float weirdFillUpTimeThingie = _baseFillUpTime * Mathf.Pow ( _fillUpTimeModifier, lvl );
		
		MaxFood = (int)(GatheringSpeed * weirdFillUpTimeThingie);
	}
}
