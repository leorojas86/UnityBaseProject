using UnityEngine;
using System.Collections;

public abstract class CharacterInput 
{
	#region Variables
	
	protected float _rotation   		= 0;
	protected Vector3 _movement 		= Vector3.zero;
	protected bool _isJumpingButtonDown = false;
	
	#endregion
	
	#region Properties
	
	public float Rotation
	{
		get { return _rotation; }
		set { _rotation = value; }
	}
	
	public Vector3 Movement 
	{
		get { return _movement; }
	}
	
	public bool IsJumpingButtonDown
	{
		get { return _isJumpingButtonDown; }
	}
	
	#endregion

	#region Constructors

	public CharacterInput()
	{
	}

	#endregion

	#region Methods

	public abstract void UpdateInput();

	public virtual bool Detect()
	{
		return Movement != Vector3.zero || _isJumpingButtonDown;
	}

	#endregion
}
