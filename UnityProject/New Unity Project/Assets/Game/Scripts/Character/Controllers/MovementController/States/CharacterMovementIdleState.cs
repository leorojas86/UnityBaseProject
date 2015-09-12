using UnityEngine;
using System.Collections;

public class CharacterMovementMovingState : CharacterMovementState
{
    #region Variables

    protected bool _lastBendToogleState = false;

    #endregion

    #region Constructors

    public CharacterMovementMovingState(Character character) : base(character)
    {
        _character = character;
    }

    #endregion

    #region Methods

    public bool GoToBendingState()
    {
        if(_character.Input != null && _character.IsLanded)
        {
            if (_lastBendToogleState != _character.Input.IsBendToogle)
            {
                _lastBendToogleState = _character.Input.IsBendToogle;
                return true;
            }
        }

        return false;
    }

    public bool GoToJumpingState()
    {
        return _character.Input != null && _character.IsLanded && _character.Input.IsJumpTriggered;
    }

    #endregion
}
