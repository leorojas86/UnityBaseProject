using UnityEngine;
using System.Collections;

public class CharacterMovementMovingState : CharacterMovementState
{
    #region Variables


    #endregion

    #region Constructors

    public CharacterMovementMovingState(Character character) : base(character)
    {
    }

    #endregion

    #region Methods

    public override void OnExecute()
    {
        base.OnExecute();

        UpdateMovementInput();
        UpdateRotationInput();
    }

    #endregion

    #region Transitions

    protected virtual void UpdateMovementInput()
    {
        if(_character.Input.TargetMovement != Vector3.zero)
        {
            Vector3 targetVelocity          = _character.Input.TargetMovement * _character.MovementSpeed;
            float movementLerp              = GetMovementLerp();
            Vector3 newVelocity             = Vector3.Lerp(_character.RigidBody.velocity, targetVelocity, movementLerp);
            newVelocity.y                   = _character.RigidBody.velocity.y; //Keep gravity movement, only change x,z
            _character.RigidBody.velocity   = newVelocity;
        }
    }

    protected virtual float GetMovementLerp()
    {
        if(_character.IsLanded)
        {
            if(_character.Input.IsBendToogle)
                return Constants.CHARACTER_MOVEMENT_LERP_SPEED * Constants.CHARACTER_MOVEMENT_BEND_MULTIPLIER;

            return Constants.CHARACTER_MOVEMENT_LERP_SPEED;
        }

        return Constants.CHARACTER_MOVEMENT_LERP_SPEED * Constants.CHARACTER_MOVEMENT_FLYING_MULTIPLIER;
    }

    protected virtual void UpdateRotationInput()
    {
        if(_character.firstPersonCamera != null)
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

    #endregion
}
