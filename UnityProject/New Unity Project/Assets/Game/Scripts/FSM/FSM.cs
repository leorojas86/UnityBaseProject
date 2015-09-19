using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSM
{
	#region Variables
	
	/// <summary>
	/// Current execution state.
	/// </summary>
	protected FSMState _currentState = null;
	
	/// <summary>
	/// Determines whether the state machine has been paused.
	/// </summary>
	protected bool _isPaused  = false;
	
	/// <summary>
	/// Determines whether debug info should be written to console.
	/// </summary>
	protected bool _isDebugInfoEnabled = false; 

	/// <summary>
	/// The states of the machine
	/// </summary>
	protected List<FSMState> _states = new List<FSMState>();

	public System.Action<FSM> OnStateChanged = null;
	
	#endregion
	
	#region Properties
	
	/// <summary>
	/// Manages entering and exiting a state.
	/// </summary>
	/// <value>
	/// State to transition to.
	/// </value>
	public virtual FSMState CurrentState
	{
		get { return _currentState; }
		set
		{
			if(_currentState != null)
				_currentState.OnExit(value);

			FSMState previousState = _currentState;
			_currentState 			  = value;

			if(_currentState != null) 
			{
				if(_isDebugInfoEnabled)
					Debug.Log("Calling OnEnter on " + _currentState.GetType().Name);

				_currentState.OnEnter(previousState);
			}

			if(OnStateChanged != null)
				OnStateChanged(this);
		}
	}
	
	/// <summary>
	/// Determines if the FSM is paused.
	/// </summary>
	/// <value>
	/// <c>true</c> if the FSM is paused; otherwise, <c>false</c>.
	/// </value>/
	public virtual bool IsPaused
	{
		get { return _isPaused; }
		set
		{
			_isPaused = value;
		
			if(_currentState != null)
		   		_currentState.OnPause(value);
		}
	}
	
	/// <summary>
	/// Determines if debug info should be written to console.
	/// </summary>
	/// <value>
	/// <c>true</c> if writing debug info to console is enabled; otherwise, <c>false</c>.
	/// </value>
	public virtual bool IsDebugInfoEnabled
	{
		get { return _isDebugInfoEnabled; }
		set { _isDebugInfoEnabled = value; }
	}

	/// <summary>
	/// Gets or sets the states.
	/// </summary>
	/// <value>The states.</value>
	public List<FSMState> States
	{
		get { return _states; }
		set { _states = value; }
	}

	#endregion
	
	#region Methods
	
	/// <summary>
	/// Updates the FSM and allows a transition to occur. This method should only be called from a FixedUpdate method when applicable.
	/// </summary>
	public virtual void Update()// Step must be called from FixedUpdate method
	{
		if(!_isPaused && _currentState != null)
		{
			_currentState.OnExecute();
			FSMState nextState = _currentState.OnCondition();
			
			if(nextState != null)
				CurrentState = nextState;
		}
	}

	public virtual void AddState(FSMState state)
	{
		_states.Add(state);
	}

	public virtual T GetState<T>() where T : FSMState
	{
		return GetState(typeof(T)) as T;
	}

	public virtual FSMState GetState(System.Type stateType)
	{
		for(int i = 0; i < _states.Count; i++)
		{
			FSMState state = _states[i];

			if(state.GetType() == stateType)
				return state;
		}
		
		return null;
	}

	public virtual void GotoState<T>() where T : FSMState
	{
		GotoState(typeof(T));
	}

	public virtual void GotoState(System.Type stateType)
	{
		FSMState state = GetState(stateType);
		
		if(state != null)
			CurrentState = state;
		else
			Debug.LogError("State of type = " + stateType + " was not found on FSM, please make sure that it was added to the 'States' list."); 
	}

	public virtual void Release()
	{
		OnStateChanged = null;
		CurrentState   = null;

		for(int i = 0; i < _states.Count; i++)
			_states[i].Release();

		_states.Clear();
	}
	
	#endregion
}

