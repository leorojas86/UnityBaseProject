using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Character))]
public class CharacterMovementController : MonoBehaviour 
{
	#region Variables

	public Camera characterCamera = null;

	private float _speed			= Constants.CHARACTER_DEFAULT_SPEED;
	private Character _character	= null;
	private bool _isGoingDown		= false;
	private bool _isGoingUp			= false;
	private bool _isLanded 			= false;

	private bool _isInitialized = false;

	#endregion

	#region Properties

	public bool IsFalling
	{
		get { return _isGoingDown; }
	}

	public bool IsJumping
	{
		get { return _isGoingUp; }
	}

	public bool IsLanded
	{
		get { return _isLanded; }
	}

	#endregion

	#region Methods

	void Awake()
	{
		_character = GetComponent<Character>();
	}

	private void Initialize()
	{
		_character.Input.YRotation       = transform.rotation.eulerAngles.y;
		_character.Input.XRotation       = characterCamera != null ? characterCamera.transform.localRotation.eulerAngles.z : transform.rotation.eulerAngles.z;
		_character.RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotation;
	}
	
	void Update() 
	{
		if(_character.Input != null)
		{
			if(!_isInitialized)
			{
				Initialize();
				_isInitialized = true;
			}

			UpdateLandedFlag();

			//Debug.Log("_isJumping = " + _isJumping + " _isFalling = " + _isFalling + " _rigidBody.velocity.y = " + _rigidBody.velocity.y);

			_character.Input.UpdateInput();

			UpdateMovement();
			UpdateRotation();
			CheckForJump();
		}
	}

	private void UpdateLandedFlag()
	{
		if(_isLanded)
			_isLanded = Mathf.Abs(_character.RigidBody.velocity.y) < Constants.CHARACTER_MAX_LANDED_Y_VELOCITY;
		else
		{
			_isGoingUp   = _character.RigidBody.velocity.y > Constants.CHARACTER_MAX_LANDED_Y_VELOCITY;
			_isGoingDown = _character.RigidBody.velocity.y < -Constants.CHARACTER_MAX_LANDED_Y_VELOCITY;
			//_isLanded    = !_isGoingUp && !_isGoingDown;
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(100,0,1000,1000), "_rigidBody.velocity.y = " + _character.RigidBody.velocity.y + " _isLanded = " + _isLanded +  " _isGoingDown = " + _isGoingDown + " _isGoingUp = " + _isGoingUp);
	}

	private void CheckForJump()
	{
		if(_character.Input.IsJumpButtonDown && IsLanded)
		{
			Vector3 velocity    		  = _character.RigidBody.velocity;
			velocity.y 		    		  += Constants.CHARACTER_JUMP_FORCE;
			_character.RigidBody.velocity = velocity; 
			_isGoingUp 					  = true;
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
			Vector3 targetVelocity    	  = _character.Input.Movement * _speed;
			float lerp			      	  = IsLanded ? Constants.CHARACTER_MOVEMENT_LERP_SPEED : Constants.CHARACTER_MOVEMENT_LERP_SPEED * Constants.CHARACTER_MOVEMENT_FLYING_MULTIPLIER;
			Vector3 newVelocity 	  	  = Vector3.Lerp(_character.RigidBody.velocity, targetVelocity, lerp);
			newVelocity.y		  	  	  = _character.RigidBody.velocity.y; //Keep gravity movement, only change x,z
			_character.RigidBody.velocity = newVelocity;
		}
	}

	private void UpdateRotation()
	{
		if(characterCamera != null)
		{	
			characterCamera.transform.localRotation  = Quaternion.Lerp(characterCamera.transform.localRotation, Quaternion.Euler(_character.Input.XRotation, 0, 0), Constants.CHARACTER_MOVEMENT_LERP_SPEED);
			_character.RigidBody.rotation   	 				 = Quaternion.Lerp(_character.RigidBody.rotation, Quaternion.Euler(0, _character.Input.YRotation, 0), Constants.CHARACTER_MOVEMENT_LERP_SPEED);
		}
		else
			_character.RigidBody.rotation = Quaternion.Lerp(_character.RigidBody.rotation, Quaternion.Euler(_character.Input.XRotation, _character.Input.YRotation, 0), Constants.CHARACTER_MOVEMENT_LERP_SPEED);
	}

	public void Reset()
	{
		_character.RigidBody.velocity = Vector3.zero;
	}

	void OnCollisionEnter(Collision collision)
	{
		CheckForTerrainCollision(collision);
	}

	void OnCollisionStay(Collision collision)
	{
		CheckForTerrainCollision(collision);
	}

	private void CheckForTerrainCollision(Collision collision)
	{
		if(!_isLanded)
		{
			_isLanded = collision.collider.gameObject.layer == LayerMask.NameToLayer("Terrain");
			
			if(_isLanded)
			{
				_isGoingUp   = false;
				_isGoingDown = false;
			}
		}
	}

	#endregion
}
