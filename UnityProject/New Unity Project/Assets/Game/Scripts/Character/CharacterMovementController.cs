using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Character))]
public class CharacterMovementController : MonoBehaviour 
{
	#region Variables

	private Rigidbody _rigidBody 	= null;
	private float _speed			= Constants.CHARACTER_DEFAULT_SPEED;
	private Character _character	= null;
	private bool _isFalling			= false;
	private bool _isJumping			= false;
	private int _fallingFrames	    = 5;

	#endregion

	#region Properties

	public bool IsFalling
	{
		get { return _isFalling; }
	}

	public bool IsJumping
	{
		get { return _isJumping; }
	}

	public bool IsLanded
	{
		get { return !_isFalling && !_isJumping; }
	}

	#endregion

	#region Methods

	void Awake()
	{
		_rigidBody   		      = GetComponentInChildren<Rigidbody>();
		_character 			      = GetComponent<Character>();
		_character.Input.YRotation = transform.rotation.eulerAngles.y;
		_rigidBody.constraints    = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotation;
	}
	
	void Update() 
	{
		UpdateJumpingAndFallingFlags();

		//Debug.Log("_isJumping = " + _isJumping + " _isFalling = " + _isFalling + " _rigidBody.velocity.y = " + _rigidBody.velocity.y);

		_character.Input.UpdateInput();

		UpdateMovement();
		CheckForJump();
	}

	private void UpdateJumpingAndFallingFlags()
	{
		if(_isJumping)
		{
			_isJumping = _rigidBody.velocity.y > Constants.CHARACTER_MIN_FALLING_Y_VELOCITY;
			
			if(!_isJumping)
			{
				_fallingFrames = 0;
				_isFalling     = true;
			}
		}
		else if(_isFalling)
		{
			_fallingFrames++;
			
			if(_fallingFrames > 3 && Mathf.Abs(_rigidBody.velocity.y) < Constants.CHARACTER_MIN_FALLING_Y_VELOCITY)
				_isFalling = false;
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(100,0,1000,1000), "_isFalling = " + _isFalling + " _isJumping = " + _isJumping);
	}

	private void CheckForJump()
	{
		if(_character.Input.IsJumpButtonDown && IsLanded)
		{
			Vector3 velocity    = _rigidBody.velocity;
			velocity.y 		    += Constants.CHARACTER_JUMP_FORCE;
			_rigidBody.velocity = velocity; 
			_isJumping 			= true;
		}
	}

	/*private bool IsLanded()
	{
		float belowCollisionDistance = GetBelowCollisionDistance();

		//Debug.Log("belowCollisionDistance = " + belowCollisionDistance);

		return belowCollisionDistance < Constants.CHARACTER_MIN_LANDED_FLOOR_DISTANCE;
	}

	private float GetBelowCollisionDistance()
	{
		RaycastHit hit;

		if(Physics.Raycast(transform.position, -Vector3.up, out hit)) 
			return hit.distance;

		return float.MaxValue;
	}*/
	
	private void UpdateMovement()
	{
		if(_character.Input.Movement != Vector3.zero)
		{
			Vector3 targetVelocity    = _character.Input.Movement * _speed;
			float lerp			      = IsLanded ? Constants.CHARACTER_MOVEMENT_LERP_SPEED : Constants.CHARACTER_MOVEMENT_LERP_SPEED * Constants.CHARACTER_MOVEMENT_FLYING_MULTIPLIER;
			Vector3 newVelocity 	  = Vector3.Lerp(_rigidBody.velocity, targetVelocity, lerp);
			newVelocity.y		  	  = _rigidBody.velocity.y; //Keep gravity movement, only change x,z
			_rigidBody.velocity 	  = newVelocity;
		}

		_rigidBody.rotation = Quaternion.Lerp(_rigidBody.rotation, Quaternion.Euler(0, _character.Input.YRotation, 0), Constants.CHARACTER_MOVEMENT_LERP_SPEED);
	}

	public void Reset()
	{
		_rigidBody.velocity = Vector3.zero;
	}

	#endregion
}
