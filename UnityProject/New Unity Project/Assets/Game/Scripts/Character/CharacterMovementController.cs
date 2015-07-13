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
		_rigidBody   		   = GetComponentInChildren<Rigidbody>();
		_character 			   = GetComponent<Character> ();
		_rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotation;

		_character.Input.SetMovementRotation(transform.rotation.y);
	}
	
	void Update() 
	{
		UpdateMovement();

		bool isLanded = IsLanded();

		//Debug.Log(isLanded);

		if(isLanded && _character.Input.IsJumpButtonDown())
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
		Vector3 movement = _character.Input.GetMovement();

		if(movement != Vector3.zero)
		{
			/*if(playerAnimation.IsPlaying("idle_anim"))
				playerAnimation.CrossFade("run_anim");*/

			Vector3 targetVelocity    = movement * _speed;
			Vector3 newVelocity 	  = Vector3.Lerp(_rigidBody.velocity, targetVelocity, Constants.CHARACTER_MOVEMENT_LERP_SPEED);
			newVelocity.y		  	  = _rigidBody.velocity.y; //Keep gravity movement, only change x,z
			_rigidBody.velocity 	  = newVelocity;

			Quaternion lookRotation    = Quaternion.LookRotation(movement);
			//_lastLookRotation		   = Quaternion.Lerp(_lastLookRotation, lookRotation, Constants.CHARACTER_MOVEMENT_LERP_SPEED);
			//Only rotate in y axis
			_rigidBody.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
		}
		else
		{
			/*if(playerAnimation.IsPlaying("run_anim"))
				playerAnimation.CrossFade("idle_anim");*/

			//_lastVelocity = Vector3.zero;
		}


	}

	public void Reset()
	{
		_rigidBody.velocity = Vector3.zero;
	}

	#endregion
}
