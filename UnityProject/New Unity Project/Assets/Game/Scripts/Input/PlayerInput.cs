using UnityEngine;
using System.Collections;

public abstract class PlayerInput 
{
	#region Variables
	
	protected float _yRotation   		= 0;
	protected float _xRotation			= 0;
	protected Vector3 _movement 		= Vector3.zero;
	protected bool _isJumpButtonDown 	= false;
	
	#endregion
	
	#region Properties
	
	public float YRotation
	{
		get { return _yRotation; }
		set { _yRotation = value; }
	}

	public float XRotation
	{
		get { return _xRotation; }
		set { _xRotation = value; }
	}
	
	public Vector3 Movement 
	{
		get { return _movement; }
	}
	
	public bool IsJumpButtonDown
	{
		get { return _isJumpButtonDown; }
	}
	
	#endregion

	#region Constructors

	public PlayerInput()
	{
	}

	#endregion

	#region Methods

	public abstract void UpdateInput();

	public abstract PlayerInput Detect();

	#endregion
}
