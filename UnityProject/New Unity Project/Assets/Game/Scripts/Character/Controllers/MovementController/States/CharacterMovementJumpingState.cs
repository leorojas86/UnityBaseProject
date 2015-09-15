using UnityEngine;
using System.Collections;

public class CharacterMovementJumpingState : CharacterMovementMovingState
{
    #region Constructors

    public CharacterMovementJumpingState(Character character) : base(character)
    {
    }

    #endregion

    #region Methods

    public override void OnEnter()
    {
        base.OnEnter();

        AddJumpVelocity();
    }

    private void AddJumpVelocity()
    {
        Vector3 velocity                = _character.RigidBody.velocity;
        velocity.y                      += Constants.CHARACTER_JUMP_FORCE;
        _character.RigidBody.velocity   = velocity;
    }

    public override void OnExecute()
    {
        base.OnExecute();

        _isCompleted = _character.IsLanded;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    #endregion

    #region Transitions

    public override bool GoToMovingState()
    {
        return _isCompleted && base.GoToMovingState();
    }

    public override bool GoToIdle()
    {
        return _isCompleted && base.GoToIdle();
    }

    public override bool CanBend()
    {
        return false;
    }

    public override bool CanJump()
    {
        return false;
    }

    #endregion
}
