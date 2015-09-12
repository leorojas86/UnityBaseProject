using UnityEngine;
using System.Collections;

public abstract class PlayerInput 
{
	#region Variables

    protected Character _character    = null;
	protected Vector3 _targetRotation = Vector3.zero;
	protected Vector3 _targetMovement = Vector3.zero;
	protected bool _isJumpTriggered   = false;
	protected bool _isBendToogle      = false;
    protected bool _isDisabled        = false;
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
        set { _targetMovement = value; }
	}
	
	public bool IsJumpTriggered
	{
		get { return _isJumpTriggered; }
        set { _isJumpTriggered = value; }
	}

	public bool IsBendToogle
	{
		get { return _isBendToogle; }
        set { _isBendToogle = value;  }
	}

    public Character Character
    {
        get { return _character; }
        set 
        { 
            _character = value;

            if(_character != null)
                UpdateTargetRotationToCurrent();
        }
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

    protected virtual void UpdateTargetRotationToCurrent()
    {
        float initialZRotation          = _character.firstPersonCamera != null ? _character.firstPersonCamera.transform.localRotation.eulerAngles.z : _character.transform.rotation.eulerAngles.z;
        _character.Input.TargetRotation = new Vector3(_character.transform.rotation.eulerAngles.x, _character.transform.rotation.eulerAngles.y, initialZRotation);
    }

	public abstract void UpdateInput();

	public abstract PlayerInput Detect();

    public virtual void ClearLastInput()
    {
        _targetMovement  = Vector3.zero;
        _isJumpTriggered = false;

        UpdateTargetRotationToCurrent();
    }

	#endregion
}
