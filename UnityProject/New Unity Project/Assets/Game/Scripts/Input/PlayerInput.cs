using UnityEngine;
using System.Collections;

public abstract class PlayerInput 
{
	#region Variables

    protected Character _character       = null;
    protected Quaternion _targetRotation = Quaternion.identity;
	protected Vector3 _targetMovement    = Vector3.zero;
	protected bool _isJumpTriggered      = false;
	protected bool _isBendToogle         = false;
    protected bool _isDisabled           = false;
    //protected bool _isBreakToogle     = false;
	
	#endregion
	
	#region Properties

	public Quaternion TargetRotation
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
                _targetRotation = _character.InputRotation;
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

	public abstract void UpdateInput();

	public abstract PlayerInput Detect();

    public virtual void ClearLastInput()
    {
        _targetMovement  = Vector3.zero;
        _isJumpTriggered = false;
        _targetRotation  = _character.InputRotation;
    }

	#endregion
}
