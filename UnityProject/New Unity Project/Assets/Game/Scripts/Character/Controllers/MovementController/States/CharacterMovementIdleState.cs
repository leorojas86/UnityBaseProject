using UnityEngine;
using System.Collections;

public class CharacterMovementDefaultState : CharacterMovementState
{
    #region Variables

    protected bool _lastBendToogleState = false;

    #endregion

    #region Constructors

    public CharacterMovementDefaultState(Character character) : base(character)
    {
        _character = character;
    }

    #endregion

    #region Methods

    public bool GoToBendingState()
    {
        if(_character.Input != null)
        {
            if (_lastBendToogleState != _character.Input.IsBendToogle)
            {
                _lastBendToogleState = _character.Input.IsBendToogle;
                return true;
            }
        }

        return false;
    }

    #endregion
}
