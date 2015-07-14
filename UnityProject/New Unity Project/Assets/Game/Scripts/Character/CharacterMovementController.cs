using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Character))]
public class CharacterMovementController : MonoBehaviour 
{
	#region Variables

	private Rigidbody _rigidBody 	    	= null;
	private float _speed					= Constants.CHARACTER_DEFAULT_SPEED;
	private Character _character			= null;
	//private float _targetRotation			= 0;

	#endregion

	#region Methods

	void Awake()
	{
		_rigidBody   		      = GetComponentInChildren<Rigidbody>();
		_character 			      = GetComponent<Character>();
		_character.Input.Rotation = transform.rotation.y;
		_rigidBody.constraints    = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotation;
	}
	
	void Update() 
	{
		_character.Input.UpdateInput();

		UpdateMovement();

		bool isLanded = IsLanded();

		//Debug.Log(isLanded);

		if(isLanded && _character.Input.IsJumpingButtonDown)
		{
			Vector3 velocity    = _rigidBody.velocity;
			velocity.y 		    += 10;
			_rigidBody.velocity = velocity; 
		}
	}

	private bool IsLanded()
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
	}
	
	private void UpdateMovement()
	{
		if(_character.Input.Movement != Vector3.zero)
		{
			Vector3 targetVelocity    = _character.Input.Movement * _speed;
			Vector3 newVelocity 	  = Vector3.Lerp(_rigidBody.velocity, targetVelocity, Constants.CHARACTER_MOVEMENT_LERP_SPEED);
			newVelocity.y		  	  = _rigidBody.velocity.y; //Keep gravity movement, only change x,z
			_rigidBody.velocity 	  = newVelocity;
		}

		_rigidBody.rotation = Quaternion.Lerp(_rigidBody.rotation, Quaternion.Euler(0, _character.Input.Rotation, 0), Constants.CHARACTER_MOVEMENT_LERP_SPEED);
	}

	public void Reset()
	{
		_rigidBody.velocity = Vector3.zero;
	}

	#endregion
}
