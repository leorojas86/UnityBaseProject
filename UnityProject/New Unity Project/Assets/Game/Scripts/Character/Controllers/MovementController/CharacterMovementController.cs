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
        CharacterMovementIdleState idleState          = new CharacterMovementIdleState(_character);
        CharacterMovementMovingState movingStateState = new CharacterMovementMovingState(_character);
        CharacterMovementBendingState bendingState    = new CharacterMovementBendingState(_character);
        CharacterMovementJumpingState jumpingState    = new CharacterMovementJumpingState(_character);

        idleState.AddTransition(bendingState, idleState.GoToBendingState);
        idleState.AddTransition(jumpingState, idleState.GoToJumpingState);
        idleState.AddTransition(movingStateState, idleState.GoToMovingState);

        movingStateState.AddTransition(bendingState, movingStateState.GoToBendingState);
        movingStateState.AddTransition(jumpingState, movingStateState.GoToJumpingState);
        movingStateState.AddTransition(idleState, movingStateState.GoToIdle);

        bendingState.AddTransition(idleState, bendingState.IsCompleted);

        jumpingState.AddTransition(movingStateState, jumpingState.GoToMovingState);
        jumpingState.AddTransition(idleState, jumpingState.GoToIdle);

        _fsm.CurrentState       = idleState;//Initial State
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
            _fsm.Update();
	}

    public bool CanBend()
    {
        return (_fsm.CurrentState as CharacterMovementState).CanBend();
    }

    public bool CanJump()
    {
        return (_fsm.CurrentState as CharacterMovementState).CanJump();
    }

	public void Reset()
	{
		_character.RigidBody.velocity    = Vector3.zero;
        _character.RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotation;
	}

	#endregion
}
