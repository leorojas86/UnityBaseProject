using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Character))]
public class CharacterMovementController : MonoBehaviour 
{
	#region Variables

	private float _speed		 = Constants.CHARACTER_DEFAULT_SPEED;
	private Character _character = null;

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
		float initialZRotation 			 = _character.firstPersonCamera != null ? _character.firstPersonCamera.transform.localRotation.eulerAngles.z : transform.rotation.eulerAngles.z;
		_character.Input.TargetRotation  = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, initialZRotation);
		_character.RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotation;
	}
	
	void Update() 
	{
		if(_character.Input != null)
		{
			UpdateMovement();
			UpdateRotation();
            
            if(_character.IsLanded)
            {
                CheckForJump();
                UpdateBend();
                UpdateBreak();
            }
		}
	}

    private void UpdateBreak()
    {
        if(_character.Input.IsBreakToogle)
        {
            Vector3 velocity              = _character.RigidBody.velocity;
            Vector3 breakVelocity         = Vector3.Lerp(velocity, Vector3.zero, Constants.CHARACTER_BREAK_LERP);
            _character.RigidBody.velocity = breakVelocity;
        }
    }

    private void UpdateBend()
    {
        float targetXRotation                           = _character.Input.IsBendToogle ? Constants.CHARACTER_BEND_X_ROTATION : Constants.CHARACTER_STAND_X_ROTATION;
        Vector3 currentRotation                         = _character.Capsule.transform.localEulerAngles;
        currentRotation.x                               = Mathf.Lerp(currentRotation.x, targetXRotation, Constants.CHARACTER_MOVEMENT_LERP_SPEED);
        _character.Capsule.transform.localEulerAngles   = currentRotation;
    }

	private void CheckForJump()
	{
		if(_character.Input.IsJumpTriggered)
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
            float movementLerp            = GetMovementLerp();
			Vector3 newVelocity 	  	  = Vector3.Lerp(_character.RigidBody.velocity, targetVelocity, movementLerp);
			newVelocity.y		  	  	  = _character.RigidBody.velocity.y; //Keep gravity movement, only change x,z
			_character.RigidBody.velocity = newVelocity;
		}
	}

    private float GetMovementLerp()
    {
        if(_character.IsLanded)
        {
            if(_character.Input.IsBendToogle)
                return Constants.CHARACTER_MOVEMENT_LERP_SPEED * Constants.CHARACTER_MOVEMENT_BEND_MULTIPLIER;

            return Constants.CHARACTER_MOVEMENT_LERP_SPEED;
        }

        return Constants.CHARACTER_MOVEMENT_LERP_SPEED * Constants.CHARACTER_MOVEMENT_FLYING_MULTIPLIER;
    }

	private void UpdateRotation()
	{
        if (_character.firstPersonCamera != null)
        {
            _character.firstPersonCamera.transform.localRotation = Quaternion.Lerp(_character.firstPersonCamera.transform.localRotation, Quaternion.Euler(_character.Input.TargetRotation.z, 0, 0), Constants.CHARACTER_MOVEMENT_LERP_SPEED);
            Quaternion targetRigbodyRotation                     = PhysicsUtils.SetQuaternionYAxis(_character.RigidBody.rotation, _character.Input.TargetRotation.y);
            _character.RigidBody.rotation                        = Quaternion.Lerp(_character.RigidBody.rotation, targetRigbodyRotation, Constants.CHARACTER_MOVEMENT_LERP_SPEED);
        }
        else
        {
            Quaternion targetRigbodyRotation = PhysicsUtils.SetQuaternionXAndYAxis(_character.RigidBody.rotation, _character.Input.TargetRotation.z, _character.Input.TargetRotation.y);
            _character.RigidBody.rotation    = Quaternion.Lerp(_character.RigidBody.rotation, targetRigbodyRotation, Constants.CHARACTER_MOVEMENT_LERP_SPEED);
        }
	}

	public void Reset()
	{
		_character.RigidBody.velocity    = Vector3.zero;
        _character.RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotation;
	}

	#endregion
}
