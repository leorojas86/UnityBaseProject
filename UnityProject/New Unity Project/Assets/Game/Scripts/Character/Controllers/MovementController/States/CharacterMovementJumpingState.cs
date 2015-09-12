using UnityEngine;
using System.Collections;

public class CharacterMovementJumpingState : CharacterMovementState
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

    #endregion
}
