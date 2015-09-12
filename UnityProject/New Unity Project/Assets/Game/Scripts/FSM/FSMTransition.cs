using UnityEngine;
using System.Collections;

public sealed class FSMTransition
{
	#region Delegates

	/// <summary>
	/// Delegate method that defines actions to be made once a transition is invoked.
	/// </summary>
	public delegate bool OnTransitionDelegate();
	
	#endregion
	
	#region Variables

	/// <summary>
	/// Method to execute whenever a transition is invoked.
	/// </summary>
	private OnTransitionDelegate _delegate;
	
	/// <summary>
	/// The state to transition to.
	/// </summary>
	private FSMState _toState;
	
	#endregion
	
	#region Constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="FSMTransition"/> class.
	/// </summary>
	/// <param name='toState'>
	/// State to trnasition to.
	/// </param>
	/// <param name='delegateFunction'>
	/// Code to execute on transition.
	/// </param>
	public FSMTransition(FSMState toState, OnTransitionDelegate delegateFunction)
	{
		_toState   = toState; 
		_delegate  = delegateFunction;
	}
	
	#endregion
	
	#region Properties

	/// <summary>
	/// Gets the state to transition to.
	/// </summary>
	/// <value>
	/// The state to transition to.
	/// </value>
	public FSMState ToState
	{
		get { return _toState; }
	}
	
	#endregion
	
	#region Methods

	/// <summary>
	/// Checks whether the transition should be made or not.
	/// </summary>
	public bool Check()
	{
		return _delegate();
	}
	
	#endregion
}

