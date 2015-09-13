using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterMovementController 
{
	#region Variables

	private Character _character               = null;
    private FSM _fsm                           = new FSM();
    private Quaternion _initialRotation        = Quaternion.identity;
    private Quaternion _initialCameraRotation  = Quaternion.identity;

	#endregion

	#region Properties

    public Quaternion InitialRotation
    {
        get { return _initialRotation; }
    }

    public Quaternion InitialCameraRotation
    {
        get { return _initialCameraRotation; }
    }

	#endregion

    #region Constructors

    public CharacterMovementController(Character character)
    {
        _character                          = character;
        _initialRotation                    = _character.Rotation;
        _initialCameraRotation              = _character.CameraRotation;
        _character.RigidBody.constraints    = RigidbodyConstraints.FreezeRotation;

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
        //_fsm.IsDebugInfoEnabled = true;
    }

    #endregion

    #region Methods
	
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
        _character.Rotation              = _initialRotation;
        _character.CameraRotation        = _initialCameraRotation;
    }

	#endregion
}
