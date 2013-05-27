using UnityEngine;
using System.Collections.Generic;

public abstract class StateManager : MonoBehaviour {

	public Stack<State> stateStack;
	
	// Use this for initialization
	public void Start () {
		//stateStack = new Stack<State>();
	}
	
	// Update is called once per frame
	public void Update () {
		stateStack.Peek().OnExecute();
	}
	
	public Stack<State> GetStack()
	{
		return stateStack;
	}
	
	public State GetPeek()
	{
		return GetStack().Peek();
	}
	
	public void SetStack(Stack<State> stack)
	{
		stateStack = stack;	
	}
	
	public void ChangeState(State state)
	{
		if (stateStack.Count == 0)
			PushState(state);
		else
		{
			stateStack.Peek().OnStop();
			stateStack.Pop();
			
			stateStack.Push(state);
			stateStack.Peek().OnStart();
		}
	}
	
	public void PushState(State state)
	{
		if (stateStack.Count > 0) stateStack.Peek().OnPause();
		
		stateStack.Push(state);
		stateStack.Peek().OnStart();
	}
	
	public void PopState()
	{
		if (stateStack.Count > 1)
		{
			stateStack.Peek().OnStop();
			stateStack.Pop();
			
			stateStack.Peek().OnContinue();
		}
	}
}
