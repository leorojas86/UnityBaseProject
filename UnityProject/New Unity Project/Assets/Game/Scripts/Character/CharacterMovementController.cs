using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Character))]
public class CharacterMovementController : MonoBehaviour 
{
	#region Variables

	private Rigidbody _rigidBody 	    = null;
	private float _lastLookRotation 	= 0;
	private Vector3 _lastVelocity       = Vector3.zero;
	private float _speed				= Constants.CHARACTER_DEFAULT_SPEED;
	private Character _character		= null;

	#endregion

	#region Methods

	void Awake()
	{
		_rigidBody   		   = GetComponentInChildren<Rigidbody>();
		_character 			   = GetComponent<Character> ();
		_rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotation;
	}
	
	void Update() 
	{
		Vector3 movement = _character.Input.GetMovement();

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
		Vector3 velocity    = movement * _speed;
		_lastVelocity 		= Vector3.Lerp(_lastVelocity, velocity, Constants.CHARACTER_MOVEMENT_LERP_SPEED);
		_lastVelocity.y     = _rigidBody.velocity.y;//Keep gravity movement, only change x,z
		_rigidBody.velocity = _lastVelocity;

		if(movement != Vector3.zero)
		{
			/*if(playerAnimation.IsPlaying("idle_anim"))
				playerAnimation.CrossFade("run_anim");*/

			Quaternion lookRotation    = Quaternion.LookRotation(velocity);
			Vector3 lookRotationVector = lookRotation.eulerAngles;
			_lastLookRotation		   = MathUtils.LinearBezier(_lastLookRotation, lookRotationVector.y, Constants.CHARACTER_MOVEMENT_LERP_SPEED);
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
