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

    

    #endregion

    #region Transitions

    public bool GoToIdle()
    {
        return _character.Input != null && _character.IsLanded && _character.Input.TargetMovement == Vector3.zero;
    }

    #endregion
}
