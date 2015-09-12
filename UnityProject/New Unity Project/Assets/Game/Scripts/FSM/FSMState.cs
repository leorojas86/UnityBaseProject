using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSMState
{
	#region Variables
	
	/// <summary>
	/// Determines whether the state has completed excecution.
	/// </summary>
	protected bool _isCompleted = false;
	
	/// <summary>
	/// List of possible transitions from this state to another.
	/// </summary>
	protected List<FSMTransition> _transitions = new List<FSMTransition>();
	
	#endregion
	
	#region Properties

	[System.Obsolete("public List<F2UFSMTransition> Transitions is deprecated, use AddTransition method instead.")]
	public List<FSMTransition> Transitions
	{
		get { return _transitions; }
		set { _transitions = value; }
	}
	
	#endregion
	
	#region Methods

	/// <summary>
	/// Code to be executed when the state starts.
	/// </summary>
	public virtual void OnEnter(FSMState previousState)
	{
		OnEnter();
	}
	
	/// <summary>
	/// Code to be executed when the state transitions.
	/// </summary>
	public virtual void OnExit(FSMState nextState)
	{
		OnExit();
	}

	
	/// <summary>
	/// Code to be executed when the state starts.
	/// </summary>
	public virtual void OnEnter()
	{
        CleanVariables();
	}
	
	/// <summary>
	/// Code to be executed while the state is active.
	/// </summary>
	public virtual void OnExecute()
	{
	}
	
	/// <summary>
	/// Code to be executed when the state transitions.
	/// </summary>
	public virtual void OnExit()
	{
        CleanVariables();
	}

	public virtual void AddTransition(FSMState toState, FSMTransition.OnTransitionDelegate delegateFunction)
	{
		_transitions.Add(new FSMTransition(toState, delegateFunction));
	}
	
	/// <summary>
	/// Checks whether the state should transition to another.
	/// </summary>
	/// <returns>
	/// The next state if able to do a transition, otherwise <c>null</c>
	/// </returns>
	public virtual FSMState OnCondition()
	{
		for(int i = 0; i < _transitions.Count; i++)
		{
			FSMTransition transition = _transitions[i];

			if(transition.Check())
				return transition.ToState;
		}
		
		return null;
	}
	
	/// <summary>
	/// Code to be executed when the FSM pauses.
	/// </summary>
	/// <param name='pause'>
	/// Pause state.
	/// </param>
	public virtual void OnPause(bool pause)
	{
		
	}

	/// <summary>
	/// Method to trigger transition when the state is completed
	/// </summary>
	public bool IsCompleted()
	{
		return _isCompleted;
	}

    protected virtual void CleanVariables()
    {
        _isCompleted = false;//Variable to trigger transition when the state is completed
    }

	public virtual void Release()
	{
		_transitions.Clear();
	}
	
	#endregion
}

