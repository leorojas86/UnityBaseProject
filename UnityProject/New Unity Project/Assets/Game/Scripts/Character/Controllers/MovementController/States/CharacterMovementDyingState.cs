using UnityEngine;
using System.Collections;

public class CharacterMovementDyingState : CharacterMovementState
{
    #region Constructors

    public CharacterMovementDyingState(Character character):base(character)
    {

    }

    #endregion

    #region Methods

    public override void OnEnter()
    {
        base.OnEnter();

        _character.Reset();
        _isCompleted = true;
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
