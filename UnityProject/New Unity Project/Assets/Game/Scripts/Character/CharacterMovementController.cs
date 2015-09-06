using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Character))]
public class CharacterMovementController : MonoBehaviour 
{
	#region Variables

	private float _speed			= Constants.CHARACTER_DEFAULT_SPEED;
	private Character _character	= null;

	#endregion

	#region Properties

	#endregion

	#region Methods

	void Awake()
	{
		_character = GetComponent<Character>();
	}

	public void OnPlayerInputDetected()
	{
		float initialYRotation 			 = transform.rotation.eulerAngles.y;
		float initialZRotation 			 = _character.firstPersonCamera != null ? _character.firstPersonCamera.transform.localRotation.eulerAngles.z : transform.rotation.eulerAngles.z;
		_character.Input.TargetRotation  = new Vector3(0, initialYRotation, initialZRotation);
		_character.RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotation;
	}
	
	void Update() 
	{
		if(_character.Input != null)
		{
			UpdateMovement();
			UpdateRotation();
			CheckForJump();
		}
	}

	private void CheckForJump()
	{
		if(_character.Input.IsJumpButtonDown && _character.IsLanded)
		{
			Vector3 velocity    		  = _character.RigidBody.velocity;
			velocity.y 		    		  += Constants.CHARACTER_JUMP_FORCE;
			_character.RigidBody.velocity = velocity;
		}
	}
	
	private void UpdateMovement()
	{
		if(_character.Input.TargetMovement != Vector3.zero)
		{
			Vector3 targetVelocity    	  = _character.Input.TargetMovement * _speed;
			float lerp			      	  = _character.IsLanded ? Constants.CHARACTER_MOVEMENT_LERP_SPEED : Constants.CHARACTER_MOVEMENT_LERP_SPEED * Constants.CHARACTER_MOVEMENT_FLYING_MULTIPLIER;
			Vector3 newVelocity 	  	  = Vector3.Lerp(_character.RigidBody.velocity, targetVelocity, lerp);
			newVelocity.y		  	  	  = _character.RigidBody.velocity.y; //Keep gravity movement, only change x,z
			_character.RigidBody.velocity = newVelocity;
		}
	}

	private void UpdateRotation()
	{
		if(_character.firstPersonCamera != null)
		{	
			_character.firstPersonCamera.transform.localRotation    = Quaternion.Lerp(_character.firstPersonCamera.transform.localRotation, Quaternion.Euler(_character.Input.TargetRotation.z, 0, 0), Constants.CHARACTER_MOVEMENT_LERP_SPEED);
			_character.RigidBody.rotation   	 	 				= Quaternion.Lerp(_character.RigidBody.rotation, Quaternion.Euler(0, _character.Input.TargetRotation.y, 0), Constants.CHARACTER_MOVEMENT_LERP_SPEED);
		}
		else
			_character.RigidBody.rotation = Quaternion.Lerp(_character.RigidBody.rotation, Quaternion.Euler(_character.Input.TargetRotation.z, _character.Input.TargetRotation.y, 0), Constants.CHARACTER_MOVEMENT_LERP_SPEED);
	}

	public void Reset()
	{
		_character.RigidBody.velocity = Vector3.zero;
	}

	#endregion
}
