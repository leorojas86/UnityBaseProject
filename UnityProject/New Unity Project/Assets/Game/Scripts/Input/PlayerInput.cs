using UnityEngine;
using System.Collections;

public abstract class PlayerInput 
{
	#region Variables

	protected Vector3 _targetRotation = Vector3.zero;
	protected Vector3 _targetMovement = Vector3.zero;
	protected bool _isJumpTriggered   = false;
	protected bool _isBendToogle      = false;
    //protected bool _isBreakToogle     = false;
	
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
	
	public bool IsJumpTriggered
	{
		get { return _isJumpTriggered; }
	}

	public bool IsBendToogle
	{
		get { return _isBendToogle; }
	}

    /*public bool IsBreakToogle
    {
        get { return _isBreakToogle; }
    }*/
	
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
