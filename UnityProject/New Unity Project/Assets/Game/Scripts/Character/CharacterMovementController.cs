using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterMovementController : MonoBehaviour 
{
	#region Variables
	
	private CharacterInput _playerInput     = null;
	private Rigidbody _rigidBody 	    	= null;
	private float _lastLookRotation 		= 0;
	private float _speed					= Constants.CHARACTER_DEFAULT_SPEED;

	#endregion

	#region Methods

	void Awake()
	{
		_rigidBody   			= GetComponentInChildren<Rigidbody>();
		_rigidBody.constraints  = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		_playerInput 			= new KeyboardCharacterInput();
	}
	
	void Update() 
	{
		Vector3 movement = _playerInput.GetMovement();

		UpdateMovement(movement);
	}

	private bool IsLanded()
	{
		float belowCollisionDistance = GetBelowCollisionDistance();

		return belowCollisionDistance < Constants.CHARACTER_MIN_LANDED_FLOOR_DISTANCE;
	}

	private float GetBelowCollisionDistance()
	{
		RaycastHit hit;

		if(Physics.Raycast(transform.position, -Vector3.up, out hit)) 
			return hit.distance;

		return float.MaxValue;
	}
	
	private void UpdateMovement(Vector3 movement)
	{
		Vector3 velocity   = movement * _speed;
		velocity.y 		   = _rigidBody.velocity.y;
		_rigidBody.velocity = velocity;

		if(movement != Vector3.zero)
		{
			/*if(playerAnimation.IsPlaying("idle_anim"))
				playerAnimation.CrossFade("run_anim");*/

			Quaternion lookRotation    = Quaternion.LookRotation(velocity);
			Vector3 lookRotationVector = lookRotation.eulerAngles;
			_lastLookRotation		   = lookRotationVector.y;
			_rigidBody.rotation 	   = Quaternion.Euler(0, _lastLookRotation, 0);
		}
		else
		{
			/*if(playerAnimation.IsPlaying("run_anim"))
				playerAnimation.CrossFade("idle_anim");*/

			_rigidBody.rotation = Quaternion.Euler(0, _lastLookRotation, 0);
		}
	}

	public void Reset()
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		UpdateMovement(Vector3.zero);//Update stand position
	}

	#endregion
}
