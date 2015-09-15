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

        if(_character.PhysicsController.IsLanded)
            UpdateBreak();
    }

    private void UpdateBreak()
    {
        _character.Velocity = Vector3.Lerp(_character.Velocity, Vector3.zero, Constants.CHARACTER_BREAK_LERP);
    }

    #endregion
}
