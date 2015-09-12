using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterMovementController 
{
	#region Variables

	private Character _character = null;
    private FSM _fsm             = new FSM();

	#endregion

	#region Properties

	#endregion

    #region Constructors

    public CharacterMovementController(Character character)
    {
        _character = character;

        InitializeFSM();
    }

    private void InitializeFSM()
    {
        CharacterMovementDefaultState defaultState = new CharacterMovementDefaultState(_character);
        CharacterMovementBendingState bendingState = new CharacterMovementBendingState(_character);

        defaultState.AddTransition(bendingState, defaultState.GoToBendingState);

        bendingState.AddTransition(defaultState, bendingState.IsCompleted);

        _fsm.CurrentState = defaultState;//Initial State

        _fsm.IsDebugInfoEnabled = true;
    }

    #endregion

    #region Methods


	public void OnPlayerInputDetected()
	{
        float initialZRotation              = _character.firstPersonCamera != null ? _character.firstPersonCamera.transform.localRotation.eulerAngles.z : _character.transform.rotation.eulerAngles.z;
        _character.Input.TargetRotation     = new Vector3(_character.transform.rotation.eulerAngles.x, _character.transform.rotation.eulerAngles.y, initialZRotation);
		_character.RigidBody.constraints    = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotation;
	}
	
	public void Update() 
	{
		if(_character.Input != null)
		{
			UpdateMovement();
			UpdateRotation();
            
            if(_character.IsLanded)
            {
                CheckForJump();
                UpdateBreak();
            }
		}

        _fsm.Update();
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
            Vector3 targetVelocity        = _character.Input.TargetMovement * _character.MovementSpeed;
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
