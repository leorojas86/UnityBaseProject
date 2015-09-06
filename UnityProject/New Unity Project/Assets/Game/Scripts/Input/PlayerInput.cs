using UnityEngine;
using System.Collections;

public abstract class PlayerInput 
{
	#region Variables

	protected Vector3 _targetRotation = Vector3.zero;
	protected Vector3 _targetMovement = Vector3.zero;
	protected bool _isJumpButtonDown  = false;
	protected bool _isBendButtonDown  = false;
	
	#endregion
	
	#region Properties

	public Vector3 TargetRotation
	{
		get { return _targetRotation; }
		set { _targetRotation = value; }
	} 
	
	public Vector3 TargetMovement 
	{
		get { return _targetMovement; }
	}
	
	public bool IsJumpButtonDown
	{
		get { return _isJumpButtonDown; }
	}

	public bool IsBendButtonDown
	{
		get { return _isBendButtonDown; }
	}
	
	#endregion

	#region Constructors

	protected PlayerInput()
	{
	}

	#endregion

	#region Methods

	public abstract void UpdateInput(Vector3 currentCharacterPosition);

	public abstract PlayerInput Detect();

	#endregion
}
