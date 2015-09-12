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
        CharacterMovementMovingState defaultState  = new CharacterMovementMovingState(_character);
        CharacterMovementBendingState bendingState = new CharacterMovementBendingState(_character);
        CharacterMovementJumpingState jumpingState = new CharacterMovementJumpingState(_character);

        defaultState.AddTransition(bendingState, defaultState.GoToBendingState);
        defaultState.AddTransition(jumpingState, defaultState.GoToJumpingState);

        bendingState.AddTransition(defaultState, bendingState.IsCompleted);

        jumpingState.AddTransition(defaultState, jumpingState.IsCompleted);

        _fsm.CurrentState       = defaultState;//Initial State
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
            if(_character.IsLanded)
                UpdateBreak();

            _fsm.Update();
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

	public void Reset()
	{
		_character.RigidBody.velocity    = Vector3.zero;
        _character.RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotation;
	}

	#endregion
}
