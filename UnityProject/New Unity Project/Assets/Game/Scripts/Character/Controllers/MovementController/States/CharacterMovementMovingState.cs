﻿using UnityEngine;
using System.Collections;

public class CharacterMovementMovingState : CharacterMovementState
{
	#region Variables

	//private float _lastRotationDifference = 0;

	#endregion

    #region Constructors

    public CharacterMovementMovingState(Character character) : base(character)
    {
    }

    #endregion

    #region Methods

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExecute()
    {
        base.OnExecute();

        UpdateMovementInput();
        UpdateInputRotation();
    }

    protected virtual void UpdateMovementInput()
    {
        if(_character.Input.TargetMovement != Vector3.zero)
        {
            Vector3 targetVelocity  = _character.Input.TargetMovement * _character.StatsController.MovementSpeed.value;
            float movementLerp      = GetMovementLerp();
            Vector3 newVelocity     = Vector3.Lerp(_character.Velocity, targetVelocity, movementLerp);
            newVelocity.y           = _character.Velocity.y; //Keep gravity movement, only change x,z
            _character.Velocity     = newVelocity;
        }
    }

    protected virtual float GetMovementLerp()
    {
        if(_character.PhysicsController.IsLanded)
            return _character.PhysicsController.IsBended ? Constants.CHARACTER_MOVEMENT_LERP_SPEED * Constants.CHARACTER_MOVEMENT_BEND_MULTIPLIER : Constants.CHARACTER_MOVEMENT_LERP_SPEED;

        return Constants.CHARACTER_MOVEMENT_LERP_SPEED * Constants.CHARACTER_MOVEMENT_FLYING_MULTIPLIER;
    }

    protected virtual void UpdateInputRotation()
    {
        if(_character.Input.TargetRotation != Vector2.zero)
        {
            Quaternion targetRotation 	= _character.MovementController.InitialRotation * Quaternion.AngleAxis(_character.Input.TargetRotation.x, Vector3.up);
            _character.Rotation 		= Quaternion.Lerp(_character.Rotation, targetRotation, Constants.CHARACTER_MOVEMENT_LERP_SPEED);

			//_lastRotationDifference = Quaternion.Angle(_character.Rotation, targetRotation);

			//Debug.Log("_lastRotationDifference = " + _lastRotationDifference);

            Quaternion targetCameraRotation = _character.MovementController.InitialCameraRotation * Quaternion.AngleAxis(_character.Input.TargetRotation.y, Vector3.left);
            _character.CameraRotation 		= Quaternion.Lerp(_character.CameraRotation, targetCameraRotation, Constants.CHARACTER_MOVEMENT_LERP_SPEED);
        }
    }

    #endregion

    #region Transitions

	public override bool GoToIdle()
	{
		return base.GoToIdle() /*&& _lastRotationDifference < 1*/;
	}

    #endregion
}
