using UnityEngine;
using System.Collections;

public class CharacterMovementMovingState : CharacterMovementState
{
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
            return _character.IsBended ? Constants.CHARACTER_MOVEMENT_LERP_SPEED * Constants.CHARACTER_MOVEMENT_BEND_MULTIPLIER : Constants.CHARACTER_MOVEMENT_LERP_SPEED;

        return Constants.CHARACTER_MOVEMENT_LERP_SPEED * Constants.CHARACTER_MOVEMENT_FLYING_MULTIPLIER;
    }

    protected virtual void UpdateInputRotation()
    {
        Quaternion targetRotation = _character.MovementController.InitialInputRotation * Quaternion.AngleAxis(_character.Input.TargetRotation.x, Vector3.up) * Quaternion.AngleAxis(_character.Input.TargetRotation.y, Vector3.left);
        _character.InputRotation  = Quaternion.Lerp(_character.InputRotation, targetRotation, Constants.CHARACTER_MOVEMENT_LERP_SPEED);
    }

    #endregion
}
