using UnityEngine;
using System.Collections;

public class FSMBehaviour : MonoBehaviour 
{
	#region Enums

	/// <summary>
	/// Enumeration used to associate a type of update method for the FSM.
	/// </summary>
	public enum FSMUpdateMethod
	{
		Update,
		FixedUpdate,
		None
	}

	#endregion

	#region Variables

	/// <summary>
	/// Base FSM to manage.
	/// </summary>
	protected FSM _fsm 					= null;
	protected FSMUpdateMethod _updateMethod = FSMUpdateMethod.Update;

	#endregion
	
	#region Properties

	/// <summary>
	/// Gets the FSM.
	/// </summary>
	/// <value>
	/// The FSM.
	/// </value>
	public FSM FSM
	{
		get { return _fsm; }
	}

	/// <summary>
	/// Gets or sets the update method.
	/// </summary>
	/// <value>The update method.</value>
	public FSMUpdateMethod UpdateMethod
	{
		get { return _updateMethod; }
		set { _updateMethod = value; }
	}
	
	#endregion
	
	#region Methods
	
	/// <summary>
	/// Used to update the FSM state.
	/// </summary>
	protected virtual void Update() 
	{
		if(_updateMethod == FSMUpdateMethod.Update)
			InternalUpdate();
	}

	/// <summary>
	/// Used to updated the FSM state in the FixedUpdate.
	/// </summary>
	protected virtual void FixedUpdate() 
	{
		if(_updateMethod == FSMUpdateMethod.FixedUpdate)
			InternalUpdate();
	}

	protected virtual void InternalUpdate()
	{
		if(_fsm != null)
			_fsm.Update();
	}

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	protected virtual void OnDestroy()
	{
		if(_fsm != null)
		{
			_fsm.Release();
			_fsm = null;
		}
	}
	
	#endregion
}
