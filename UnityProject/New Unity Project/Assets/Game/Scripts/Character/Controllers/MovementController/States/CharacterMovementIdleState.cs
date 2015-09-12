using UnityEngine;
using System.Collections;

public class CharacterMovementIdleState : CharacterMovementState
{
    #region Constructors

    public CharacterMovementIdleState(Character character) : base(character)
    {
    }

    #endregion

    #region Methods

    public override void OnExecute()
    {
        base.OnExecute();

        if(_character.IsLanded)
            UpdateBreak();
    }

    private void UpdateBreak()
    {
        Vector3 velocity                = _character.RigidBody.velocity;
        Vector3 breakVelocity           = Vector3.Lerp(velocity, Vector3.zero, Constants.CHARACTER_BREAK_LERP);
        _character.RigidBody.velocity   = breakVelocity;
    }

    #endregion

    #region Transitions

    public bool GoToMovingState()
    {
        return _character.IsLanded && _character.Input != null && _character.Input.TargetMovement != Vector3.zero;
    }

    #endregion
}
